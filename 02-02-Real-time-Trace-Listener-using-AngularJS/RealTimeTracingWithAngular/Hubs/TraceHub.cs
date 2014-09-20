using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace RealTimeTracingWithAngular.Hubs
{
    public class TraceHub : Hub
    {
    }

    public class SignalRTraceListener : TraceListener
    {
        private void WriteToSignalR(string message)
        {
            var traceHub = 
                GlobalHost.ConnectionManager.GetHubContext<TraceHub>();
            traceHub.Clients.All.traceReceived(message);
        }

        public override void Write(string message)
        {
            WriteToSignalR(message);
        }

        public override void WriteLine(string message)
        {
            WriteToSignalR(message);
        }
    }
}