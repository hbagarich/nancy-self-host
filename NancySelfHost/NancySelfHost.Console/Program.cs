using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancySelfHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostController = new HostController();
            hostController.StartServer();

            System.Console.ReadLine();
        }
    }
}
