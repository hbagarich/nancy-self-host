using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Cors;
using Nancy.Bootstrapper;
using Nancy.Owin;
using Owin;

namespace NancySelfHost.Middleware
{
    public class NancyMiddleware : IOwinMiddleWare
    {
        private readonly INancyBootstrapper _nancyBootstrapper;

        public NancyMiddleware(INancyBootstrapper nancyBootstrapper)
        {
            _nancyBootstrapper = nancyBootstrapper;
        }

        public int Order => 3;

        public void Attach(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(CorsOptions.AllowAll);
            var options = new NancyOptions
            {
                Bootstrapper = _nancyBootstrapper,
            };
            appBuilder.UseNancy(options);
        }
    }
}
