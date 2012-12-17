using Bifrost.NHibernate;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingNHibernate(this IHaveStorage storage, EntityContextConfiguration entityContextConfiguration)
        {
            storage.EntityContextConfiguration = entityContextConfiguration;
            return Configure.Instance;
        }
    }
}
