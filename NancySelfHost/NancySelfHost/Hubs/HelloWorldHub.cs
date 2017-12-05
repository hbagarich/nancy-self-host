using Microsoft.AspNet.SignalR;

namespace NancySelfHost.Hubs
{
    public class HelloWorldHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.broadcastMessage(message);
        }
    }
}
