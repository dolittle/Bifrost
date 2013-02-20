using System.Web.Routing;
using Bifrost.Web.Services;
using Bifrost.Web.Applications;
using Bifrost.Web.Commands;
using Bifrost.Web.Proxies;
using Bifrost.Web.Read;
using Bifrost.Web.Sagas;
using Bifrost.Web.Validation;

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
            RouteTable.Routes.AddService<QueryService>("Bifrost/Query");
            RouteTable.Routes.AddService<ReadModelService>("Bifrost/ReadModel");
        }
    }
}
