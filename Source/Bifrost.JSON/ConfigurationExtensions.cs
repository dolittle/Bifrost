using Bifrost.JSON;
using Bifrost.Entities;
using Bifrost.Events;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static Configure UsingJson(this ICommandsConfiguration commandsConfiguration, string path)
        {
            commandsConfiguration.Storage = new EntityContextConfiguration
            {
                Connection = new EntityContextConnection { Directory = path }
            };
            return Configure.Instance;
        }

        public static Configure UsingJson(this ISagasConfiguration sagasConfiguration, string path)
        {
            return Configure.Instance;
        }

        public static Configure UsingJson(this IEventsConfiguration eventsConfiguration, string path)
        {
            eventsConfiguration.RepositoryType = typeof(EventRepository);
            return Configure.Instance;
        }
            
        public static Configure UsingJsonStorage(this Configure configure, string path)
        {
            var entityContextConfiguration = new EntityContextConfiguration
            {
                Connection = new EntityContextConnection { Directory = path }
            };
            configure.Container.Bind<IEntityContextConfiguration>(entityContextConfiguration);
            configure.Container.Bind((EntityContextConnection)entityContextConfiguration.Connection);
            configure.Container.Bind(typeof(IEntityContext<>), typeof(EntityContext<>));
            configure.Commands.Storage = entityContextConfiguration;

            return configure;
        }
    }
}
