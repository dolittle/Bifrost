using System.Web.Routing;
using Bifrost.Services.Execution;
using Bifrost.Web.Commands;
using Bifrost.Web.Proxies;
using Bifrost.Web.Sagas;
using Bifrost.Web.Validation;
using Bifrost.Web.Applications;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.RouteActivator), "Start")]
namespace Bifrost.Web
{
    public class RouteActivator
    {
        public static void Start()
        {
            RouteTable.Routes.Add(new ProxyRoute());
            RouteTable.Routes.AddService<ValidationService>("Bifrost/Validation");
            RouteTable.Routes.AddService<CommandCoordinatorService>("Bifrost/CommandCoordinator");
            RouteTable.Routes.AddService<SagaNarratorService>("Bifrost/SagaNarrator");
            RouteTable.Routes.Add(new AssetManagerRoute("Bifrost/AssetsManager"));
        }
    }
}
