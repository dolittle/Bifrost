/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Entities;
using Bifrost.Concepts;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Bifrost.MongoDB
{
    public class EntityContext<T> : IEntityContext<T>
    {
        EntityContextConnection _connection;
        string _collectionName;
        MongoCollection<T> _collection;

        public EntityContext(EntityContextConnection connection)
        {
            _connection = connection;
            _collectionName = typeof(T).Name;
            if( !_connection.Database.CollectionExists(_collectionName) )
                _connection.Database.CreateCollection(_collectionName);

            _collection = _connection.Database.GetCollection<T>(_collectionName);
        }


        public IQueryable<T> Entities
        {
            get { return _collection.FindAll().AsQueryable(); }
        }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
            _collection.Insert(entity);
        }

        public void Update(T entity)
        {
            Save(entity);
        }

        public void Delete(T entity)
        {
        }

        public void Save(T entity)
        {
            _collection.Save(entity);
        }

        public void Commit()
        {
        }

        public void Dispose()
        {
        }


        public T GetById<TProperty>(TProperty id)
        {
            var objectId = GetObjectId(id);
            return _collection.FindOneById(objectId);
        }

        BsonValue GetObjectId<TProperty>(TProperty id)
        {
            object idValue = id;

            if (id.IsConcept()) idValue = id.GetConceptValue();

            var idAsValue = BsonValue.Create(idValue);
            return idAsValue;
        }


        public void DeleteById<TProperty>(TProperty id)
        {
            var objectId = GetObjectId(id);
            _collection.Remove(Query.EQ("_id", objectId));
        }
    }
}
