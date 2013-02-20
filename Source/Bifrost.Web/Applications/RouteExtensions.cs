using System.Reflection;
using System.Web.Routing;
using Bifrost.Web.Applications;

namespace System.Web.Routing
{
    public static class RouteExtensions
    {
        public static void AddApplicationFromAssembly(this RouteCollection routes, string url, Assembly assembly)
        {
            routes.Add(new ApplicationRoute(url, assembly));
        }
    }
}
