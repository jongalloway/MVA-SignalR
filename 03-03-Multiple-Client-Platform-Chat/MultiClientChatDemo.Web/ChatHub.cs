using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MultiClientChatDemo.Web
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string msg)
        {
            Clients.All.receiveMessage(msg);
        }
    }
}