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
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/main.js", "/content/main.js"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/vendor.js", "/content/vendor.js"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/manifest.js", "/content/manifest.js"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/style.css", "/content/style.css"));
            //nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("css", "/content/css", "css"));
            //nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("js", "/content/js", "js"));
            //nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("images", "/content/img"));
            //nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", "/content/fonts"));
        }
    }
}
