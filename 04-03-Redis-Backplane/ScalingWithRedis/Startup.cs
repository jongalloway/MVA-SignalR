using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(ScalingWithSql.Startup))]

namespace ScalingWithSql
{
    public class Startup
    {
        void SetupScaleOut()
        {
            GlobalHost.DependencyResolver.UseSqlServer(
                ConfigurationManager.ConnectionStrings["ScaleOutDb"].ConnectionString
                );
        }

        public void Configuration(IAppBuilder app)
        {
            SetupScaleOut();

            app.MapSignalR();
        }
    }
}
