using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HitCounter
{
    public class HitCounterHub : Hub
    {
        // don't do it this way in real life!
        static int _count;

        public void RecordHit()
        {
            _count += 1;

            Clients.All.receiveHit(_count);
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            _count -= 1;

            Clients.All.receiveHit(_count);

            return base.OnDisconnected();
        }
    }
}