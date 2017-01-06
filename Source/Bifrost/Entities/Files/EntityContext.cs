/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Bifrost.Serialization;

namespace Bifrost.Entities.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContext{T}"/> for simple file storage
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class EntityContext<T> : IEntityContext<T>
    {
        EntityContextConnection _connection;
        ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="EntityContext{T}"/>
        /// </summary>
        /// <param name="connection">Connection to use</param>
        /// <param name="serializer">Serializer used for serialization</param>
        public EntityContext(EntityContextConnection connection, ISerializer serializer)
        {
            _connection = connection;
            _serializer = serializer;
        }


#pragma warning disable 1591 // Xml Comments
        public IQueryable<T> Entities
        {
            get 
            {
                var files = _connection.GetAllFilesFor<T>();
                var entities = new List<T>();
                foreach( var file in files )
                {
                    var json = File.ReadAllText(file);
                    var entity = _serializer.FromJson<T>(json);
                    entities.Add(entity);
                }

                return entities.AsQueryable();
            }
        }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
            /*
            if (_mapper.CanMap<Document, T>())
            {
                var document = _mapper.Map<Document, T>(entity);
                //Save(document);
            }
            else*/
            {
                Save(entity);
            }
        }

        public void Update(T entity)
        {
            Save(entity);
        }

        public void Delete(T entity)
        {
            var path = _connection.GetPathFor<T>();
            var id = GetIdFrom(entity);
            var filePath = string.Format("{0}\\{1}", path, id);
            File.Delete(filePath);
        }

        public void Save(T entity)
        {
            var json = _serializer.ToJson(entity);
            var path = _connection.GetPathFor<T>();
            var id = GetIdFrom(entity);
            var filePath = string.Format("{0}\\{1}", path, id);
            File.WriteAllText(filePath, json);
        }

        public void Commit()
        {
        }

        public T GetById<TProperty>(TProperty id)
        {
            var path = _connection.GetPathFor<T>();
            var filePath = string.Format("{0}\\{1}", path, id);
            var json = File.ReadAllText(filePath);
            var entity = _serializer.FromJson<T>(json);
            return entity;
        }

        public void DeleteById<TProperty>(TProperty id)
        {
            var path = _connection.GetPathFor<T>();
            var filePath = string.Format("{0}\\{1}", path, id);
            File.Delete(filePath);
        }

        public void Dispose()
        {
        }


        object GetIdFrom(T entity)
        {
            var propertyInfo = typeof(T).GetTypeInfo().GetProperty("Id", BindingFlags.Public|BindingFlags.Instance);
            if (propertyInfo == null) throw new ArgumentException(string.Format("Entity of type '{0}' does not have an Id property holding the unique Id of the entity. The File provider is very simple and only supports this as a scenario, it is a demo provider not meant for use in production", typeof(T).FullName));

            return propertyInfo.GetValue(entity);
        }

#pragma warning restore 1591 // Xml Comments
    }
}
