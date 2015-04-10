using System;
using NHibernate;

namespace Bifrost.NHibernate.Read
{
    public class ReadOnlySessionProxy : ISession
    {
        readonly ISession _actualImplementation;
        const string NOT_ALLOWED = "Not allowed in a read only session";

        public ReadOnlySessionProxy(ISession actualImplementation)
        {
            _actualImplementation = actualImplementation;
        }

        public EntityMode ActiveEntityMode
        {
            get { return _actualImplementation.ActiveEntityMode; }
        }

        public ITransaction BeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return _actualImplementation.BeginTransaction(isolationLevel);
        }

        public ITransaction BeginTransaction()
        {
            return _actualImplementation.BeginTransaction();
        }

        public CacheMode CacheMode
        {
            get
            {
               return  _actualImplementation.CacheMode;
            }
            set { _actualImplementation.CacheMode = value; }
        }

        public void CancelQuery()
        {
            _actualImplementation.CancelQuery();
        }

        public void Clear()
        {
            _actualImplementation.Clear();
        }

        public System.Data.IDbConnection Close()
        {
            return _actualImplementation.Close();
        }

        public System.Data.IDbConnection Connection
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Contains(object obj)
        {
            throw new System.NotImplementedException();
        }

        public ICriteria CreateCriteria(string entityName, string alias)
        {
            throw new System.NotImplementedException();
        }

        public ICriteria CreateCriteria(string entityName)
        {
            throw new System.NotImplementedException();
        }

        public ICriteria CreateCriteria(System.Type persistentClass, string alias)
        {
            throw new System.NotImplementedException();
        }

        public ICriteria CreateCriteria(System.Type persistentClass)
        {
            throw new System.NotImplementedException();
        }

        public ICriteria CreateCriteria<T>(string alias) where T : class
        {
            throw new System.NotImplementedException();
        }

        public ICriteria CreateCriteria<T>() where T : class
        {
            throw new System.NotImplementedException();
        }

        public IQuery CreateFilter(object collection, string queryString)
        {
            throw new System.NotImplementedException();
        }

        public IMultiCriteria CreateMultiCriteria()
        {
            throw new System.NotImplementedException();
        }

        public IMultiQuery CreateMultiQuery()
        {
            throw new System.NotImplementedException();
        }

        public IQuery CreateQuery(string queryString)
        {
            throw new System.NotImplementedException();
        }

        public ISQLQuery CreateSQLQuery(string queryString)
        {
            throw new System.NotImplementedException();
        }

        public bool DefaultReadOnly
        {
            get { return _actualImplementation.DefaultReadOnly; }
            set
            {
                throw new NotSupportedException(NOT_ALLOWED);
            }
        }

        public int Delete(string query, object[] values, global::NHibernate.Type.IType[] types)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public int Delete(string query, object value, global::NHibernate.Type.IType type)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public int Delete(string query)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Delete(string entityName, object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Delete(object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void DisableFilter(string filterName)
        {
            _actualImplementation.DisableFilter(filterName);
        }

        public System.Data.IDbConnection Disconnect()
        {
            return _actualImplementation.Disconnect();
        }

        public IFilter EnableFilter(string filterName)
        {
            return _actualImplementation.EnableFilter(filterName);
        }

        public void Evict(object obj)
        {
            _actualImplementation.Evict(obj);
        }

        public void Flush()
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public FlushMode FlushMode
        {
            get
            {
                return _actualImplementation.FlushMode;
            }
            set
            {
                throw new InvalidOperationException(NOT_ALLOWED);
            }
        }

        public T Get<T>(object id, LockMode lockMode)
        {
            return _actualImplementation.Get<T>(id, lockMode);
        }

        public T Get<T>(object id)
        {
            return _actualImplementation.Get<T>(id);
        }

        public object Get(string entityName, object id)
        {
            return _actualImplementation.Get(entityName,id);
        }

        public object Get(System.Type clazz, object id, LockMode lockMode)
        {
            return _actualImplementation.Get(clazz,id,lockMode);
        }

        public object Get(System.Type clazz, object id)
        {
            return _actualImplementation.Get(clazz,id);
        }

        public LockMode GetCurrentLockMode(object obj)
        {
            return _actualImplementation.GetCurrentLockMode(obj);
        }

        public IFilter GetEnabledFilter(string filterName)
        {
            return _actualImplementation.GetEnabledFilter(filterName);
        }

        public string GetEntityName(object obj)
        {
            return _actualImplementation.GetEntityName(obj);
        }

        public object GetIdentifier(object obj)
        {
            return _actualImplementation.GetIdentifier(obj);
        }

        public IQuery GetNamedQuery(string queryName)
        {
            return _actualImplementation.GetNamedQuery(queryName);
        }

        public ISession GetSession(EntityMode entityMode)
        {
            return new ReadOnlySessionProxy(_actualImplementation.GetSession(entityMode));
        }

        public global::NHibernate.Engine.ISessionImplementor GetSessionImplementation()
        {
            return _actualImplementation.GetSessionImplementation();
        }

        public bool IsConnected
        {
            get { return _actualImplementation.IsConnected; }
        }

        public bool IsDirty()
        {
            return _actualImplementation.IsDirty();
        }

        public bool IsOpen
        {
            get { return _actualImplementation.IsOpen; }
        }

        public bool IsReadOnly(object entityOrProxy)
        {
            return _actualImplementation.IsReadOnly(entityOrProxy);
        }

        public void Load(object obj, object id)
        {
            _actualImplementation.Load(obj, id);
        }

        public object Load(string entityName, object id)
        {
            return _actualImplementation.Load(entityName,id);
        }

        public T Load<T>(object id)
        {
            return _actualImplementation.Load<T>(id);
        }

        public T Load<T>(object id, LockMode lockMode)
        {
            return _actualImplementation.Load<T>(id, lockMode);
        }

        public object Load(System.Type theType, object id)
        {
            return _actualImplementation.Load(theType, id);
        }

        public object Load(string entityName, object id, LockMode lockMode)
        {
            return _actualImplementation.Load(entityName, id, lockMode);
        }

        public object Load(System.Type theType, object id, LockMode lockMode)
        {
            return _actualImplementation.Load(theType, id, lockMode);
        }

        public void Lock(string entityName, object obj, LockMode lockMode)
        {
            _actualImplementation.Lock(entityName,obj,lockMode);
        }

        public void Lock(object obj, LockMode lockMode)
        {
            _actualImplementation.Lock(obj,lockMode);
        }

        public T Merge<T>(string entityName, T entity) where T : class
        {
            return _actualImplementation.Merge<T>(entity);
        }

        public T Merge<T>(T entity) where T : class
        {
            return _actualImplementation.Merge<T>(entity);
        }

        public object Merge(string entityName, object obj)
        {
            return _actualImplementation.Merge(entityName, obj);
        }

        public object Merge(object obj)
        {
            return _actualImplementation.Merge(obj);
        }

        public void Persist(string entityName, object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Persist(object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public IQueryOver<T, T> QueryOver<T>(string entityName, System.Linq.Expressions.Expression<System.Func<T>> alias) where T : class
        {
            return QueryOver<T>(entityName, alias);
        }

        public IQueryOver<T, T> QueryOver<T>(string entityName) where T : class
        {
            return _actualImplementation.QueryOver<T>(entityName);
        }

        public IQueryOver<T, T> QueryOver<T>(System.Linq.Expressions.Expression<System.Func<T>> alias) where T : class
        {
            return QueryOver<T>(alias);
        }

        public IQueryOver<T, T> QueryOver<T>() where T : class
        {
            return _actualImplementation.QueryOver<T>();
        }

        public void Reconnect(System.Data.IDbConnection connection)
        {
            _actualImplementation.Reconnect(connection);
        }

        public void Reconnect()
        {
            _actualImplementation.Reconnect();
        }

        public void Refresh(object obj, LockMode lockMode)
        {
            _actualImplementation.Refresh(obj,lockMode);
        }

        public void Refresh(object obj)
        {
            _actualImplementation.Refresh(obj);
        }

        public void Replicate(string entityName, object obj, ReplicationMode replicationMode)
        {
            _actualImplementation.Replicate(entityName,obj,replicationMode);
        }

        public void Replicate(object obj, ReplicationMode replicationMode)
        {
            _actualImplementation.Replicate(obj,replicationMode);
        }

        public object Save(string entityName, object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Save(string entityName, object obj, object id)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Save(object obj, object id)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public object Save(object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void SaveOrUpdate(string entityName, object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void SaveOrUpdate(string entityName, object obj, object id)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void SaveOrUpdate(object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public object SaveOrUpdateCopy(object obj, object id)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public object SaveOrUpdateCopy(object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Update(string entityName, object obj, object id)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public ISessionFactory SessionFactory
        {
            get { throw new NotSupportedException(NOT_ALLOWED); ; }
        }

        public ISession SetBatchSize(int batchSize)
        {
            return new ReadOnlySessionProxy(_actualImplementation.SetBatchSize(batchSize));
        }

        public void SetReadOnly(object entityOrProxy, bool readOnly)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public global::NHibernate.Stat.ISessionStatistics Statistics
        {
            get { return _actualImplementation.Statistics; }
        }

        public ITransaction Transaction
        {
            get { return _actualImplementation.Transaction; }
        }

        public void Update(string entityName, object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Update(object obj, object id)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Update(object obj)
        {
            throw new NotSupportedException(NOT_ALLOWED);
        }

        public void Dispose()
        {
            _actualImplementation.Dispose();
        }
    }
}