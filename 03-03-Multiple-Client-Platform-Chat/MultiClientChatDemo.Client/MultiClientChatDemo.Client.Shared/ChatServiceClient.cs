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

            _hub.On<string,string>("receiveMessage", (sender, message) =>
                {
                    if (MessageReceived != null)
                        MessageReceived(this, new MessageReceivedEventArgs(
                            new ChatMessage
                            {
                                Sender = sender,
                                Message = message
                            }));
                });

            _hubConnection.Start();
        }

        public void SendMessage(ChatMessage message)
        {
            _hub.Invoke("sendMessage", message.Sender, message.Message);
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public ChatMessage Message { get; set; }

        public MessageReceivedEventArgs(ChatMessage message)
        {
            this.Message = message;
        }
    }
}
