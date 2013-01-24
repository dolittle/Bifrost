using System.Web;
using Bifrost.Configuration;

namespace Bifrost.Web.Proxies
{
    public class ProxyRouteHttpHandler : IHttpHandler
    {
        string _commandProxies;
        string _queryProxies;

        public ProxyRouteHttpHandler() : 
            this(
                Configure.Instance.Container.Get<CommandProxies>(),
                Configure.Instance.Container.Get<QueryProxies>()
            )
        {
        }

        public ProxyRouteHttpHandler(CommandProxies commandProxies, QueryProxies queryProxies)
        {
            _commandProxies = commandProxies.Generate();
            _queryProxies = queryProxies.Generate();
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_commandProxies);
            context.Response.Write(_queryProxies);
        }
    }
}
