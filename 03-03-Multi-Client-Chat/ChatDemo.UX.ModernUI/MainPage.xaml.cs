using Microsoft.AspNet.SignalR.Client.Hubs;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ChatDemo.UX.ModernUI
{
    public sealed partial class MainPage : Page
    {
        string _url = "http://autechedchat.azurewebsites.net/";
        MainPageViewModel _viewModel;
        IHubProxy _hub;

        public MainPage()
        {
            this.InitializeComponent();

            _viewModel = new MainPageViewModel();
            this.DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _send.IsEnabled = false;

            var con = new HubConnection(_url);
            _hub = con.CreateHubProxy("chat");

            _hub.On<string>("receiveMessage", (msg) =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        _viewModel.Messages.Insert(0, msg);
                    });
            });

            con.Start().ContinueWith(t =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        _status.Text = t.IsFaulted
                            ? "Disconnected"
                            : "Connected";

                        _send.IsEnabled = !t.IsFaulted;
                    });
            });
        }

        private void _send_Click(object sender, RoutedEventArgs e)
        {
            _hub.Invoke("SendMessage", _message.Text);
        }

        private void OnGotAccess(object sender, RoutedEventArgs e)
        {
            _message.Text = string.Empty;
        }
    }
}
