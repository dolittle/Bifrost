using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bifrost.Entities;
using Bifrost.JSON;
using Bifrost.Events;
using Ninject;
using Ninject.Web.Mvc;

namespace Bifrost.Samples.Shop.Mvc
{
	public class MvcApplication : NinjectHttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		
		protected override void OnApplicationStarted()
		{
			AreaRegistration.RegisterAllAreas();

			SetupViewLocations();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
		
		private void SetupViewLocations()
		{
			foreach (var viewEngine in ViewEngines.Engines)
			{
				if (viewEngine is VirtualPathProviderViewEngine)
				{
					var virtualPathViewEngine = viewEngine as VirtualPathProviderViewEngine;
					virtualPathViewEngine.MasterLocationFormats =
						ReplaceInStrings(virtualPathViewEngine.MasterLocationFormats, "Views", "Features");
					virtualPathViewEngine.ViewLocationFormats =
						ReplaceInStrings(virtualPathViewEngine.ViewLocationFormats, "Views", "Features");

				}
			}
		}

		private static string[] ReplaceInStrings(IEnumerable<string> strings, string partToReplace, string replaceWith)
		{
			return strings.Select(@string => @string.Replace(partToReplace, replaceWith)).ToArray();
		}


		protected override IKernel CreateKernel()
		{
			var kernel = new ConventionKernel();
			kernel.Conventions.Default();

			var config = new EntityContextConnection {Directory = "c:\\MagicalDatabase"};
			kernel.Bind<EntityContextConnection>().ToConstant(config);
			kernel.Bind(typeof(IEntityContext<>)).To(typeof(EntityContext<>)).InRequestScope();

			var dispatcher = kernel.Get<IEventDispatcher>();
			var bus = kernel.Get<InMemoryEventBus>();
			dispatcher.RegisterBus(bus);

			return kernel;
		}
	}
}