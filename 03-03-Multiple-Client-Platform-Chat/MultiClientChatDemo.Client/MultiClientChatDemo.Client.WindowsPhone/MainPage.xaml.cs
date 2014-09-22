using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MultiClientChatDemo.Client
{
    public sealed partial class MainPage : Page
    {
        ChatClientViewModel _viewModel = new ChatClientViewModel();
        ChatServiceClient _client = new ChatServiceClient();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _client.MessageReceived += _client_MessageReceived;
            _client.Connect();

            base.OnNavigatedTo(e);
        }

        async void _client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, new Windows.UI.Core.DispatchedHandler(() =>
            {
                _viewModel.Messages.Add(e.Message);
            }));
        }

        private void _send_Click(object sender, RoutedEventArgs e)
        {
            _client.SendMessage(_message.Text);
        }

        private void OnGotAccess(object sender, RoutedEventArgs e)
        {
            _message.Text = string.Empty;
        }
    }
}
