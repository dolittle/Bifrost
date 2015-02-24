#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Linq;
using Bifrost.Concepts;
using Bifrost.Entities;
using Bifrost.Extensions;
using Bifrost.Mapping;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;

namespace Bifrost.DocumentDB.Entities
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContext{T}"/> specifically for DocumentDB
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class EntityContext<T> : IEntityContext<T>
    {
        EntityContextConnection _connection;
        DocumentCollection _collection;
        IMapper _mapper;
        ICollectionStrategy _collectionStrategy;

        /// <summary>
        /// Initializes a new instance of <see cref="EntityContext{T}"/>
        /// </summary>
        /// <param name="connection"><see cref="EntityContextConnection"/> to use</param>
        /// <param name="mapper">Mapper to use for mapping objects to documents</param>
        /// <param name="collectionStrategy">Strategy used for dealing with collections</param>
        public EntityContext(EntityContextConnection connection, IMapper mapper, ICollectionStrategy collectionStrategy)
        {
            _connection = connection;
            _collectionStrategy = collectionStrategy;
            _collection = connection.GetCollectionFor(collectionStrategy.CollectionNameFor<T>());
            _mapper = mapper;
        }

#pragma warning disable 1591 // Xml Comments
        public IQueryable<T> Entities
        {
            get
            {
                var queryable = _connection.Client.CreateDocumentQuery<T>(_collection.DocumentsLink) as IQueryable<T>;
                queryable = _collectionStrategy.HandleQueryableFor<T>(queryable);
                return queryable;
            }
        }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
            //var document = _mapper.Map<Document, T>(entity);

            var documentType = typeof(T).Name;
            var document = new Document();

            var properties = typeof(T).GetProperties();
            properties.ForEach(p =>
            {
                var value = p.GetValue(entity);

                if (value.IsConcept()) value = value.GetConceptValue();

                if (p.Name.ToLowerInvariant() == "id")
                    document.Id = value.ToString();
                else
                    document.SetPropertyValue(p.Name, value);
            });
            document.SetPropertyValue("_DOCUMENT_TYPE", documentType);

            _connection.Client.CreateDocumentAsync(_collection.DocumentsLink, document);

            //_connection.Client.CreateDocumentAsync(_collection.DocumentsLink, entity);
        }

        public void Update(T entity)
        {
            _connection.Client.ReplaceDocumentAsync(_collection.DocumentsLink, entity);
        }

        public void Delete(T entity)
        {
            
        }

        public void Save(T entity)
        {
            _connection.Client.ReplaceDocumentAsync(_collection.DocumentsLink, entity);
        }

        public void Commit()
        {
        }

        public T GetById<TProperty>(TProperty id)
        {
            throw new NotImplementedException();
        }

        public void DeleteById<TProperty>(TProperty id)
        {
        }

        public void Dispose()
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}
