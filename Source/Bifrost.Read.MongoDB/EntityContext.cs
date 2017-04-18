/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Entities;
using Bifrost.Concepts;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Reflection;

namespace Bifrost.Read.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContext{T}"/> for MongoDB
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
	public class EntityContext<T> : IEntityContext<T>
	{
		EntityContextConnection _connection;
		string _collectionName;
		IMongoCollection<T> _collection;

        /// <summary>
        /// Initializes a new instance of <see cref="EntityContext{T}"/>
        /// </summary>
        /// <param name="connection"></param>
		public EntityContext(EntityContextConnection connection)
		{
			_connection = connection;
			_collectionName = typeof(T).Name;

			_collection = _connection.Database.GetCollection<T>(_collectionName);
		}

        /// <inheritdoc/>
		public IQueryable<T> Entities
		{
			get { return _collection.AsQueryable<T>(); }
		}

        /// <inheritdoc/>
		public void Attach(T entity)
		{
		}

        /// <inheritdoc/>
		public void Insert(T entity)
		{
			_collection.InsertOne(entity);
		}

        /// <inheritdoc/>
		public void Update(T entity)
		{
			Save(entity);
		}

        /// <inheritdoc/>
		public void Delete(T entity)
		{
		}

        /// <inheritdoc/>
		public void Save(T entity)
		{
			var idProperty = GetIdProperty(entity);

			var filter = Builders<T>.Filter.Eq("_id", idProperty.GetValue(entity));
			_collection.ReplaceOne(filter, entity, new UpdateOptions() { IsUpsert = true });
		}

        /// <inheritdoc/>
		public void Commit()
		{
		}

        /// <inheritdoc/>
		public T GetById<TProperty>(TProperty id)
        {
            var objectId = GetObjectId(id);
            return _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefault();
        }

        /// <inheritdoc/>
		public void DeleteById<TProperty>(TProperty id)
        {
            var objectId = GetObjectId(id);
            _collection.DeleteOne(Builders<T>.Filter.Eq("id", objectId));
        }


        /// <inheritdoc/>
		public void Dispose()
		{
		}

		PropertyInfo GetIdProperty(T entity)
		{
			return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.Name.ToLowerInvariant() == "id").First();
		}



		BsonValue GetObjectId<TProperty>(TProperty id)
		{
			object idValue = id;

			if (id.IsConcept()) idValue = id.GetConceptValue();

			var idAsValue = BsonValue.Create(idValue);
			return idAsValue;
		}
	}
}
