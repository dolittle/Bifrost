using Bifrost.Configuration;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bifrost.Web.ConfigurationActivator), "Start")]
namespace Bifrost.Web
{
    public class ConfigurationActivator
    {
        public static void Start()
        {
            Configure.DiscoverAndConfigure();
        }
    }
}
