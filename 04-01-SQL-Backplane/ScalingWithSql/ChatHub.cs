using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class ChatHub : Hub
    {
        public void SendMessage(dynamic message)
        {
            Clients.All.receiveMessage(message);
        }
    }
}