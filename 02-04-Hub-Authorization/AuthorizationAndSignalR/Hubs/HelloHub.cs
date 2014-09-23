using Microsoft.AspNet.SignalR;

namespace AuthorizationAndSignalR.Hubs
{
    //[Authorize]
    public class HelloHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}