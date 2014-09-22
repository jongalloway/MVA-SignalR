using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding4Fun.Kinect.WinForm;

namespace Kinect.Daemon
{
    class Program
    {
        private Skeleton[] _skeletonData;
        public KinectSensor _kinect;
        HubConnection _connection;
        IHubProxy _hub;

        static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public Program()
        {
            _skeletonData = new Skeleton[0];
        }

        void Start()
        {
            _connection = new HubConnection("http://localhost:24421/");
            _hub = _connection.CreateHubProxy("moveShape");

            _connection.Start().ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                        Start();
                    else
                        StartKinect();
                });
        }

        void StartKinect()
        {
            if (KinectSensor.KinectSensors.Any())
            {
                this._kinect = KinectSensor.KinectSensors.First();
                this._kinect.SkeletonStream.Enable();
                this._kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
                this._kinect.AllFramesReady += Kinect_AllFramesReady;
                this._kinect.Start();
            }
        }

        void Stop()
        {
            Log("Stopping");
            this._kinect.Stop();
            Log("Stopped");
        }

        void Kinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            _skeletonData.ToList().ForEach(s =>
                {
                    if (s.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        var rightHand = s.Joints.First(x => x.JointType == JointType.HandRight);
                        var r = rightHand.ScaleTo(800, 600);

                        _hub.Invoke("MoveShape", r.Position.X, r.Position.Y);

                        Log(string.Format("Right X: {0} Right Y: {1}",
                            r.Position.X,
                            r.Position.Y));
                    }
                });

            Array.Clear(_skeletonData, 0, _skeletonData.Length);
        }

        void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                _skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];

                if (skeletonFrame != null)
                {
                    skeletonFrame.CopySkeletonDataTo(_skeletonData);
                }
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            //p.StartKinect();
            p.Start();
            Log("Press Enter to Quit");
            Console.ReadLine();
            Log("Press Enter to Exit");
            Console.ReadLine();
            p.Stop();
        }
    }
}
