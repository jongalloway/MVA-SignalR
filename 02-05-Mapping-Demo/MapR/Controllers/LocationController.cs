using MapR.Hubs;
using Microsoft.AspNet.SignalR;
using System.Web.Http;

namespace MapR.Controllers
{
    public class LocationController : ApiController
    {
        [HttpPost]
        public void Post([FromBody] Location location)
        {
            var mappingHub = GlobalHost.ConnectionManager.GetHubContext<MappingHub>();

            var hubEventParameters = new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            mappingHub.Clients.All.locationReceived(hubEventParameters);
        }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
