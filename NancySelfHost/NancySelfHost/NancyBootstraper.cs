using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Conventions;

namespace NancySelfHost
{
    public class NancyBootstraper: DefaultNancyBootstrapper
    {
        private byte[] favicon;

        protected override byte[] FavIcon
        {
            get { return this.favicon ?? (this.favicon = LoadFavIcon()); }
        }

        private byte[] LoadFavIcon()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //Properties.Resources.app.Save(ms);
                return ms.ToArray();
            }
        }


        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Clear();
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/bundle.js", "/Content/bundle.js"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/bundle.map.js", "/Content/bundle.map.js"));
        }
    }
}
