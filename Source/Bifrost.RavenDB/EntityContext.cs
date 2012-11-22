using Bifrost.Entities;
using System.Linq;
using Raven.Client;
using System.Linq.Expressions;
using System;
using Bifrost.Extensions;

namespace Bifrost.RavenDB
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


        public T GetById<TProperty>(TProperty id)
        {
            var keyId = GetDocumentIdForType<TProperty>(id);
            return _session.Load<T>(keyId);
        }

        private string GetDocumentIdForType<TProperty>(TProperty id)
        {
            var documentKeyName = GetDocumentKeyForType();

            var keyId = string.Format("{0}/{1}", documentKeyName, id);
            return keyId;
        } 
        
        public void DeleteById<TProperty>(TProperty id)
        {
            var keyId = GetDocumentIdForType<TProperty>(id);

            _session.Advanced.DatabaseCommands.Delete(keyId, null);
        }


        private string GetDocumentKeyForType()
        {
            return _session.Advanced.DocumentStore.Conventions.GetTypeTagName(typeof(T));
        }


    


    }
}
