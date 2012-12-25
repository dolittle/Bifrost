using System;
using Bifrost.NHibernate;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingNHibernate(this IHaveStorage storage, Action<EntityContextConnection> connectionConfiguration)
        {
            var connection = new EntityContextConnection();
            connectionConfiguration(connection);
            storage.EntityContextConfiguration = new EntityContextConfiguration { Connection = connection };
            return Configure.Instance;
        }

        public static IConfigure UsingNHibernate(this IHaveStorage storage, EntityContextConfiguration entityContextConfiguration)
        {
            storage.EntityContextConfiguration = entityContextConfiguration;
            return Configure.Instance;
        }
    }
}
