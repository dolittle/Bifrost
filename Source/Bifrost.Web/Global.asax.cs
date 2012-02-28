using System.Web.Routing;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Ninject;
using Bifrost.Services.Commands;
using Bifrost.Services.Execution;
using Bifrost.Web.Mvc;
using Ninject;
using Bifrost.Services;

namespace Bifrost.Web
{
    public class Global : BifrostHttpApplication
    {
        public override void OnConfigure(Configure configure)
        {
            configure.UsingConfigConfigurationSource();

            base.OnConfigure(configure);
        }

        public override void OnStarted()
        {
            RouteTable.Routes.AddService<CommandCoordinatorService>();
            RouteTable.Routes.AddService<StuffToPersistService>();
            RouteTable.Routes.AddService<ValidationService>();
        }

        protected override IContainer CreateContainer()
        {
            var kernel = new StandardKernel();
            var container = new Container(kernel);
            return container;
        }
    }
}