using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using PersistentConnectionDemo.Features.Authorization;
using PersistentConnectionDemo.Features.SamplePersistentConnection;

[assembly: OwinStartupAttribute(typeof(PersistentConnectionDemo.Startup))]
namespace PersistentConnectionDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            app.MapSignalR<DemoPersistentConnection>("/Connections/DemoPersistentConnection");
            app.MapSignalR<AuthorizationPersistentConnection>("/Connections/AuthorizationPersistentConnection");

            app.Map("/EnableDetailedErrors", map =>
            {
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };

                map.MapSignalR(hubConfiguration);
            });
        }
    }
}
