using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(ScalingWithRedis.Startup))]

namespace ScalingWithRedis
{
    public class Startup
    {
        void SetupScaleOut()
        {
            GlobalHost.DependencyResolver.UseRedis("myredisbackplane.redis.cache.windows.net",
                6379,
                "8gUnFTrhXH8Lc4RI3PjHpfu51QHgVTMrSkogsqbZek8=",
                "RedisBackplaneDemo");
        }

        public void Configuration(IAppBuilder app)
        {
            SetupScaleOut();

            app.MapSignalR();
        }
    }
}
