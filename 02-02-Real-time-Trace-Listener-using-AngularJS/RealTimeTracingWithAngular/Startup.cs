using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealTimeTracingWithAngular.Startup))]
namespace RealTimeTracingWithAngular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
