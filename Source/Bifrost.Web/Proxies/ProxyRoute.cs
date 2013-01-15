using System.Web.Routing;

namespace Bifrost.Web.Proxies
{
    public class ProxyRoute : Route
    {
        const string ProxyUrl = "Bifrost/Proxies";

        public ProxyRoute()
            : base(ProxyUrl, new ProxyRouteHandler())
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
