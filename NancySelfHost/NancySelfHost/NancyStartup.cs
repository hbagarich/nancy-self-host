using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Engine;
using Microsoft.Owin.Hosting.Services;
using NancySelfHost.Middleware;

namespace NancySelfHost
{
    class NancyStartup
    {
        private List<IOwinMiddleWare> _owinMiddleWares;
        public NancyStartup()
        {
            loadOwinMiddleWares();
        }
        public IDisposable Start(List<string> urls)
        {
            var services = CreateServiceFactory();
            var engine = services.GetService<IHostingEngine>();

            var options = new StartOptions()
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };

            urls.ForEach(options.Urls.Add);

            var context = new StartContext(options) { Startup = BuildApp };


            try
            {
                return engine.Start(context);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException == null)
                {
                    throw;
                }

                if (ex.InnerException is HttpListenerException)
                {
                    throw;
                }

                throw ex.InnerException;
            }
        }

        private void loadOwinMiddleWares()
        {
            _owinMiddleWares = new List<IOwinMiddleWare>();
            _owinMiddleWares.Add(new SignalRMiddelware());
            _owinMiddleWares.Add(new NancyMiddleware(new NancyBootstraper()));
            _owinMiddleWares.Add(new WebApiMiddleware());
        }

        private void BuildApp(IAppBuilder appBuilder)
        {
            appBuilder.Properties["host.AppName"] = "SysKit";
            foreach (var owinMiddleWare in _owinMiddleWares.OrderBy(x => x.Order))
            {
                owinMiddleWare.Attach(appBuilder);
            }
        }

        private IServiceProvider CreateServiceFactory()
        {
            var provider = (ServiceProvider)ServicesFactory.Create();
            return provider;
        }
    }
}
