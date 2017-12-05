
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using NancySelfHost.Hubs;

namespace NancySelfHost.ApiControllers
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<HelloWorldHub>();
            context.Clients.All.broadcastMessage("singalr poruka");
            return Ok("Hello World");
        }
    }
}
