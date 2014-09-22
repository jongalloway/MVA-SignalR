using ChatDemo.UX.WindowsPhone.ViewModels;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ChatDemo.UX.WindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        string _url = "http://autechedchat.azurewebsites.net/";
        IHubProxy _hub;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _send.IsEnabled = false;

            var con = new HubConnection(_url);
            _hub = con.CreateHubProxy("chat");

            _hub.On<string>("receiveMessage", (msg) =>
            {
                Dispatcher.BeginInvoke(
                    () =>
                    {
                        App.ViewModel.Items.Insert(0, 
                            new ItemViewModel
                            {
                                LineOne = msg
                            });
                    });
            });

            con.Start().ContinueWith(t =>
            {
                Dispatcher.BeginInvoke(
                    () =>
                    {
                        _status.Text = t.IsFaulted
                            ? "Disconnected"
                            : "Connected";

                        _send.IsEnabled = !t.IsFaulted;
                    });
            });
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void _send_Click(object sender, RoutedEventArgs e)
        {
            _hub.Invoke("SendMessage", _message.Text);
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            _message.Text = string.Empty;
        }
    }
}