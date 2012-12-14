using Bifrost.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingCommonServiceLocator(this IConfigure configure)
        {
            var serviceLocator = new ContainerServiceLocator(configure.Container);
            configure.Container.Bind<IServiceLocator>(serviceLocator);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            return Configure.Instance;
        }
    }
}
