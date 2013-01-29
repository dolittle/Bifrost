using System.Web;
using Bifrost.Configuration;

namespace Bifrost.Web.Proxies
{
    public class ProxyRouteHttpHandler : IHttpHandler
    {
        string _commandProxies;
		string _readModelProxies;
        string _queryProxies;

        public ProxyRouteHttpHandler() : 
            this(
                Configure.Instance.Container.Get<CommandProxies>(),
                Configure.Instance.Container.Get<QueryProxies>(),
				Configure.Instance.Container.Get<ReadModelProxies>()
            )
        {
        }

        public ProxyRouteHttpHandler(
			CommandProxies commandProxies, 
			QueryProxies queryProxies,
			ReadModelProxies readModelProxies)
        {
            _commandProxies = commandProxies.Generate();
			_readModelProxies = readModelProxies.Generate();
            _queryProxies = queryProxies.Generate();
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            context.Response.Write(_commandProxies);
			context.Response.Write(_readModelProxies);
            context.Response.Write(_queryProxies);
        }
    }
}
