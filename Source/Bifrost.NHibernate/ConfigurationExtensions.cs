using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.NHibernate;
using Bifrost.Sagas;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingNHibernate(this IHaveStorage storage, EntityContextConfiguration entityContextConfiguration)
        {
            storage.EntityContextConfiguration = entityContextConfiguration;

            //configure.Events.RepositoryType = typeof (EventRepository);
            //configure.Sagas.LibrarianType = typeof (SagaLibrarian);

            return Configure.Instance;
        }
    }
}
