using System.Web.Routing;
using Bifrost.Web.Applications;
using Bifrost.Mimir.Web.Events;
using Bifrost.Services.Execution;
using Bifrost.Services;

namespace Bifrost.Mimir.Web
{
    public class ClassForTypeSafeConfiguration
    {
    }
}

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure WithMimir(this IConfigure configure)
        {
            RouteTable.Routes.AddApplicationFromAssembly("Mimir", typeof(Bifrost.Mimir.Web.ClassForTypeSafeConfiguration).Assembly);
            RouteTable.Routes.AddService<EventService>("Events");
            return configure;
        }
    }
}