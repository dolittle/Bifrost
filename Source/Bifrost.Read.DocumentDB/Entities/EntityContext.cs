/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Entities;
using Bifrost.Extensions;
using Bifrost.Mapping;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization.Formatters;
using System.IO;

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

        /// <summary>
        /// Initializes a new instance of <see cref="EntityContext{T}"/>
        /// </summary>
        /// <param name="connection"><see cref="EntityContextConnection"/> to use</param>
        /// <param name="mapper">Mapper to use for mapping objects to documents</param>
        public EntityContext(EntityContextConnection connection, IMapper mapper)
        {
            _connection = connection;
            _collection = connection.GetCollectionFor(typeof(T));
            _mapper = mapper;
        }

#pragma warning disable 1591 // Xml Comments
        public IQueryable<T> Entities
        {
            get
            {
                var queryable = _connection.Client.CreateDocumentQuery<T>(_collection.DocumentsLink) as IQueryable<T>;
                queryable = _connection.CollectionStrategy.HandleQueryableFor<T>(queryable);
                return queryable;
            }
        }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
            var documentType = typeof(T).Name;
            var document = new Document();

            var properties = typeof(T).GetTypeInfo().GetProperties();
            properties.ForEach(p =>
            {
                var value = p.GetValue(entity);

                if (p.PropertyType.IsConcept()) value = value.GetConceptValue();

                if (p.Name.ToLowerInvariant() == "id")
                    document.Id = value.ToString();
                else
                    document.SetPropertyValue(p.Name, value);
            });
            document.SetPropertyValue("_DOCUMENT_TYPE", documentType);

            var result = _connection.Client.CreateDocumentAsync(_collection.DocumentsLink, document).Result;
        }

        public void Update(T entity)
        {
            var properties = typeof(T).GetTypeInfo().GetProperties();
            var idProperty = properties.Where(a => a.Name.ToLowerInvariant() == "id").AsEnumerable().FirstOrDefault();
            var id = idProperty.GetValue(entity);

            Document document = _connection.Client.CreateDocumentQuery<Document>(_collection.DocumentsLink)
                .Where(r => r.Id == id.ToString())
                .AsEnumerable()
                .SingleOrDefault();

            properties.ForEach(p =>
            {
                var value = p.GetValue(entity);

                if (p.PropertyType.IsConcept()) value = value.GetConceptValue();

                if (p.Name.ToLowerInvariant() != "id")
                    document.SetPropertyValue(p.Name, value);
            });

            var result = _connection.Client.ReplaceDocumentAsync(document.SelfLink, document).Result;
        }

        public void Delete(T entity)
        {

        }

        public void Save(T entity)
        {
            var result = _connection.Client.ReplaceDocumentAsync(_collection.DocumentsLink, entity).Result;
        }

        public void Commit()
        {
        }

        public T GetById<TProperty>(TProperty id)
        {
            Document result = _connection.Client.CreateDocumentQuery<Document>(_collection.DocumentsLink, "SELECT * FROM Entities WHERE Entities.id = '" + id + "'",
                new FeedOptions { MaxItemCount = 1 })
                .AsEnumerable()
                .FirstOrDefault();

            return SerializeDocument(result);
        }

        public void DeleteById<TProperty>(TProperty id)
        {
        }

        public void Dispose()
        {
        }

        private static T SerializeDocument(Document doc)
        {
            T document;
            var serializer = new JsonSerializer()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                NullValueHandling = NullValueHandling.Ignore
            };

            if (doc != null)
            {
                document = serializer.Deserialize<T>(new JsonTextReader(new StringReader(doc.ToString())));

                return document;
            }

            return default(T);
        }

#pragma warning restore 1591 // Xml Comments
    }
}