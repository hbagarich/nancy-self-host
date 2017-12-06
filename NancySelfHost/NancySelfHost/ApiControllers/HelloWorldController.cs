
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
            return Ok("Hello World");
        }
    }
}
