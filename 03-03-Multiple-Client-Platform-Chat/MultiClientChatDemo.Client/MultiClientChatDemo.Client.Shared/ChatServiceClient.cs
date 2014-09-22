using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiClientChatDemo.Client
{
    public class ChatServiceClient
    {
        public const string SITE_URL = "http://multiclientchatsite.azurewebsites.net";
        private IHubProxy _hub;
        private HubConnection _hubConnection;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void Connect()
        {
            _hubConnection = new HubConnection(SITE_URL);
            _hub = _hubConnection.CreateHubProxy("chat");

            _hub.On<string>("receiveMessage", (msg) =>
                {
                    if (MessageReceived != null)
                        MessageReceived(this, new MessageReceivedEventArgs(msg));
                });

            _hubConnection.Start();
        }

        public void SendMessage(string message)
        {
            _hub.Invoke("sendMessage", message);
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageReceivedEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
