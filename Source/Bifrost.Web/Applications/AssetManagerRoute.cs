using System.Reflection;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public class AssetManagerRoute : Route
    {
        public AssetManagerRoute(string url) 
            : base(url, new AssetManagerRouteHandler(url))
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
