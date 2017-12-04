using Owin;

namespace NancySelfHost.Middleware
{
    public interface IOwinMiddleWare
    {
        int Order { get; }
        void Attach(IAppBuilder appBuilder);
    }
}
