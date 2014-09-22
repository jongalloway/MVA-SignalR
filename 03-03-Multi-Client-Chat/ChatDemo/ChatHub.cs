using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatDemo
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