using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Ninject;
using Bifrost.Web.Mvc;
using Ninject;
using Configure = Bifrost.Configuration.Configure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Mvc;
using Bifrost.Services;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Samples.Blog.Mvc.MvcApplication), "InitializeNinject")]


namespace Bifrost.Samples.Blog.Mvc
{
    public class MvcApplication : BifrostHttpApplication
    {
        static readonly Bootstrapper _bootstrapper = new Bootstrapper();
        protected static IKernel Kernel { get; private set; }

        public static void InitializeNinject()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            Kernel = CreateKernel();
            _bootstrapper.Initialize(() => Kernel);
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            return kernel;
        }


        protected override IContainer CreateContainer()
        {
            var container = new Container(Kernel);

            NinjectInstanceProvider.Kernel = Kernel;
            global::Ninject.Extensions.Wcf.KernelContainer.Kernel = Kernel;

            return container;
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("default.aspx");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var featuresNamespace = typeof(MvcApplication).Assembly.GetName().Name + ".Features";

            var query = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.Namespace != null && t.Namespace.StartsWith(featuresNamespace) &&
                              t.IsSubclassOf(typeof(Controller))
                        select t.Namespace;
            var namespaces = query.ToArray();
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults 
                namespaces
            );

            routes.MapRoute(
                "Home",
                "Posts/List"
                );
        }

        public override void OnConfigure(IConfigure configure)
        {
            configure.ExposeEventService();
            configure.UsingConfigConfigurationSource();
        }

        public override void OnStarted()
        {
            RelocateViews("Features");
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}