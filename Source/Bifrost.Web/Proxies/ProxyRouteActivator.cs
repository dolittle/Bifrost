using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.Proxies.ProxyRouteActivator), "Start")]
namespace Bifrost.Web.Proxies
{
    public class ProxyRouteActivator
    {
        public static void Start()
        {
            RouteTable.Routes.Add(new ProxyRoute());
        }
    }
}
