/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Microsoft.Azure.Documents;

namespace Bifrost.DocumentDB
{
    /// <summary>
    /// Represents an implementation of <see cref="ICollectionStrategy"/> that 
    /// deals with entities sitting in one collection called Entities
    /// </summary>
    public class MultipleEntitiesInOneCollection : ICollectionStrategy
    {
        const string _collectionName = "Entities";

#pragma warning disable 1591 // Xml Comments
        public string CollectionNameFor<T>()
        {
            return _collectionName;
        }

        public string CollectionNameFor(Type type)
        {
            return _collectionName;
        }

        public IQueryable<T> HandleQueryableFor<T>(IQueryable<T> queryable)
        {
            var documentType = typeof(T).Name;
            return queryable.DocumentType(documentType);
        }

        public void HandleDocumentFor<T>(Document document)
        {
            var documentType = typeof(T).Name;
            document.SetPropertyValue("_DOCUMENT_TYPE", documentType);
        }
#pragma warning restore 1591 // Xml Comments


    }
}
