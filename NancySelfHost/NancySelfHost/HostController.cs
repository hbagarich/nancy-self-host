using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancySelfHost
{
    public class HostController
    {
        private IDisposable _owinApp;
        private int _port = 7654;

        public void StartServer()
        {
            var owinAppFactory = new NancyStartup();
            _owinApp = owinAppFactory.Start(getUrls());
        }

        public void StopServer()
        {
            if (_owinApp != null)
            {
                _owinApp.Dispose();
            }
        }

        private List<string> getUrls()
        {
            var port = _port;
            List<string> urlList = new List<string>();
            urlList.Add(String.Format("http://{0}:{1}", Environment.MachineName, port));
            urlList.Add(string.Format("http://localhost:{0}", port));
            urlList.Add(string.Format("http://127.0.0.1:{0}", port));
            return urlList;
        }
    }
}
