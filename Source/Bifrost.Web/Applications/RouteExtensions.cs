using System.Reflection;
using System.Web.Routing;

namespace Bifrost.Web.Applications
{
    public static class RouteExtensions
    {
        public static void AddApplicationFromAssembly(this RouteCollection routes, string url, Assembly assembly)
        {
            routes.Add(new ApplicationRoute(url, assembly));
        }
    }
}
