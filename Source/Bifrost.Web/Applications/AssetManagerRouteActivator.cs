using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.Applications.AssetManagerRouteActivator), "Start")]
namespace Bifrost.Web.Applications
{
    public class AssetManagerRouteActivator
    {
        public static void Start()
        {
            RouteTable.Routes.Add(new AssetManagerRoute("AssetsManager"));
        }
    }
}
