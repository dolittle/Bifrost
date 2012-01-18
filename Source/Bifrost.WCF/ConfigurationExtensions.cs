using System.Web.Routing;
using Bifrost.WCF.Events;
using Bifrost.WCF.Execution;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void ExposeEventService(this IConfigure configure)
        {
            RouteTable.Routes.AddService<EventService>("Events");
        }
    }
}
