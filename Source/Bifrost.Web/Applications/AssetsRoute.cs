using System.Reflection;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public class AssetsRoute : Route
    {
        public AssetsRoute(string url) 
            : base(url, new AssetsRouteHandler(url))
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
