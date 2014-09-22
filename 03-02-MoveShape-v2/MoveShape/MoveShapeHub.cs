using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoveShape
{
    [HubName("moveShape")]
    public class MoveShapeHub : Hub
    {
        public void Init()
        {
        }

        public void MoveShape(double x, double y)
        {
            Clients.Others.shapeMoved(x, y);
        }
    }
}