using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Routing;

namespace Bifrost.WCF.Execution
{
    public static class ServiceRoutingExtensions
    {
        const string PostFix = "Service";

        public static void AddService<T>(this RouteCollection routes, string name = null, bool removePostFix = true)
        {
            routes.AddService(typeof(T), name, removePostFix);
        }

        public static void AddService(this RouteCollection routes, Type service, string name = null, bool removePostFix = true)
        {
            name = name ?? (removePostFix ? RemovePostFix(service) : service.Name);

            routes.Add(new WebApiRoute(name, new ContainerServiceHostFactory(), service));
        }


        static string RemovePostFix(this Type serviceType)
        {
            var name = serviceType.Name;
            if (name.EndsWith(PostFix))
                name = name.Substring(0, name.Length - PostFix.Length);

            return name;
        }

        static bool IsService(this Type type)
        {
            return type.GetCustomAttributes(typeof(ServiceContractAttribute), true).Length > 0;
        }

        public static void AddServicesFromNamespaceOf<T>(this RouteCollection routes, bool removePostfix = true)
        {
            var rootType = typeof(T);
            var types = rootType.Assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.StartsWith(rootType.Namespace) && t.IsService());
            foreach (var type in types)
                routes.AddService(type, removePostfix ? type.RemovePostFix() : null);
        }
    }
}