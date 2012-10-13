using System.Reflection;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public class ApplicationRoute : Route
    {
        public ApplicationRoute(string url, Assembly assembly) 
            : base(url, new ApplicationRouteHandler(url, assembly))
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
