using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace NancySelfHost
{
    public class RoutesModule : NancyModule
    {
        public RoutesModule()
        {
            Get["/"] = _ => View["index.html"];
            Post["/post"] = _ => "Hello Post";
        }
    }
}
