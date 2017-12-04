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
    public class NancyMiddleware: IOwinMiddleWare
    {
        private readonly INancyBootstrapper _nancyBootstrapper;

        public NancyMiddleware(INancyBootstrapper nancyBootstrapper)
        {
            _nancyBootstrapper = nancyBootstrapper;
        }

        public int Order => 3;

        public void Attach(IAppBuilder appBuilder)
        {
            var httpListener = (HttpListener)appBuilder.Properties["System.Net.HttpListener"];
            //httpListener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            appBuilder.UseCors(CorsOptions.AllowAll);

            httpListener.AuthenticationSchemeSelectorDelegate = request =>
            {
                return AuthenticationSchemes.Anonymous;
                if (request.Url.AbsolutePath.StartsWith("/signalr"))
                {
                    return AuthenticationSchemes.Anonymous;

                }
                else
                {
                    if ("OPTIONS".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase))
                    {
                        return AuthenticationSchemes.Anonymous;
                    }
                    else
                    {
                        return AuthenticationSchemes.Ntlm;
                    }
                }
            };
            var options = new NancyOptions
            {
                Bootstrapper = _nancyBootstrapper,
                // PerformPassThrough = context => context.Request.Path.StartsWith("/api")
            };
            appBuilder.UseNancy(options);
        }
    }
}
