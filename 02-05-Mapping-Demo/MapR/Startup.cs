using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MapR.Startup))]
namespace MapR
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
