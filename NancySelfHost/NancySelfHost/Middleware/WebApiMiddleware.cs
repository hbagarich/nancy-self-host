using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json.Serialization;
using Owin;

namespace NancySelfHost.Middleware
{
    class WebApiMiddleware : IOwinMiddleWare
    {
        public int Order => 2;

        public void Attach(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Services.Replace(typeof(IExceptionLogger), new Logger());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            appBuilder.UseWebApi(config);

        }

    }

    public class Logger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                Console.Write(context.Exception);
            }
            catch
            {
                //
            }
        }
    }
}
