#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
            Save(entity);
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
            var propertyInfo = typeof(T).GetProperty("Id", BindingFlags.Public|BindingFlags.Instance);
            if (propertyInfo == null) throw new ArgumentException(string.Format("Entity of type '{0}' does not have an Id property holding the unique Id of the entity. The File provider is very simple and only supports this as a scenario, it is a demo provider not meant for use in production", typeof(T).FullName));

            return propertyInfo.GetValue(entity);
        }

#pragma warning restore 1591 // Xml Comments
    }
}
