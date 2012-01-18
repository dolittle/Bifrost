using Bifrost.Entities;
using System.Linq;
using Raven.Client.Embedded;
using Raven.Client;

namespace Bifrost.RavenDb.Embeddable
{
    public class EntityContext<T> : IEntityContext<T>
    {
        IEntityContextConnection _connection;
        
        IDocumentSession _session;

        public EntityContext(EntityContextConnection connection)
        {
            _connection = connection;
            _session = connection.DocumentStore.OpenSession();
        }


        public IQueryable<T> Entities { get { return _session.Query<T>(); } }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
            _session.Store(entity);
        }

        public void Update(T entity)
        {
            _session.Store(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public void Save(T entity)
        {
            _session.Store(entity);
            _session.SaveChanges();
        }

        public void Commit()
        {
            _session.SaveChanges();
        }

        public void Dispose()
        {
            _session.SaveChanges();
            _session.Dispose();
        }
    }
}
