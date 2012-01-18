using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.NHibernate;
using Bifrost.Sagas;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingNHibernate(this IConfigure configure, EntityContextConfiguration entityContextConfiguration)
        {
            configure.Container.Bind<IEntityContextConfiguration>(entityContextConfiguration);
            configure.Container.Bind((EntityContextConnection)entityContextConfiguration.Connection);
            configure.Container.Bind(typeof(IEntityContext<>), typeof(EntityContext<>));
            configure.Commands.Storage = entityContextConfiguration;
            configure.Events.RepositoryType = typeof (EventRepository);
        	configure.Sagas.LibrarianType = typeof (SagaLibrarian);
            return configure;
        }
    }
}
