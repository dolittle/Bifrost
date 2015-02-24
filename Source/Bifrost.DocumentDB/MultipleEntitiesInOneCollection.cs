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
using Microsoft.Azure.Documents;

namespace Bifrost.DocumentDB
{
    /// <summary>
    /// Represents an implementation of <see cref="ICollectionStrategy"/> that 
    /// deals with entities sitting in one collection called Entities
    /// </summary>
    public class MultipleEntitiesInOneCollection : ICollectionStrategy
    {
        const string _collectionName = "Entitites";

#pragma warning disable 1591 // Xml Comments
        public string CollectionNameFor<T>()
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
