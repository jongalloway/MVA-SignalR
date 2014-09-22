using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HostApp
{
    public class ClockHub : Hub
    {
        Timer _timer;

        public override Task OnConnected()
        {
            if(_timer == null)
            {
                _timer = new Timer(1000);

                _timer.Elapsed += (s, e) =>
                    {
                        Clients.All.showTime(
                            DateTime.Now.ToString("hh:mm:ss")
                            );
                    };

                _timer.Start();
            }

            return base.OnConnected();
        }
    }
}
