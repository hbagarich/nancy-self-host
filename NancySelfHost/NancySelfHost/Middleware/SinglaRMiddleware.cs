using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace NancySelfHost.Middleware
{
    public class SignalRMiddelware : IOwinMiddleWare
    {
        public int Order => 1;

        public void Attach(IAppBuilder appBuilder)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();
            var serializer = JsonSerializer.Create(settings);

            HubConfiguration hubConfiguration = new HubConfiguration();
            hubConfiguration.Resolver.Register(typeof(JsonSerializer), () => serializer);

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.MapSignalR(hubConfiguration);
        }
    }

    public class SignalRContractResolver : IContractResolver
    {

        private readonly Assembly _assembly;
        private readonly IContractResolver _camelCaseContractResolver;
        private readonly IContractResolver _defaultContractSerializer;

        public SignalRContractResolver()
        {
            _defaultContractSerializer = new DefaultContractResolver();
            _camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
            _assembly = typeof(Connection).Assembly;
        }

        public JsonContract ResolveContract(Type type)
        {
            if (type.Assembly.Equals(_assembly))
            {
                return _defaultContractSerializer.ResolveContract(type);

            }

            return _camelCaseContractResolver.ResolveContract(type);
        }

    }
}
