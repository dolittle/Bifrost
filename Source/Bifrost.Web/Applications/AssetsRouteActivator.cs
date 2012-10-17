using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.Applications.AssetsRouteActivator), "Start")]
namespace Bifrost.Web.Applications
{
    public class AssetsRouteActivator
    {
        public static void Start()
        {
            RouteTable.Routes.Add(new AssetsRoute("Assets"));
        }
    }
}
