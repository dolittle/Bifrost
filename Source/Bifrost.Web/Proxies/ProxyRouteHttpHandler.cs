using System.Web;
using Bifrost.Configuration;

namespace Bifrost.Web.Proxies
{
    public class ProxyRouteHttpHandler : IHttpHandler
    {
        string _commandProxies;

        public ProxyRouteHttpHandler() : this(Configure.Instance.Container.Get<CommandProxies>())
        {
        }

        public ProxyRouteHttpHandler(CommandProxies commandProxies)
        {
            _commandProxies = commandProxies.Generate();
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_commandProxies);
        }
    }
}
