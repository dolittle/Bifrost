#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.Collections.Generic;
using System.Linq;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Extensions;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;

namespace Bifrost.RavenDB
{
    public class EntityContextConnection : IEntityContextConnection
    {
        EntityContextConfiguration _configuration;

        public string Url { get; set; }
        public DocumentStore DocumentStore { get; private set; }

        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            _configuration = configuration;

            Url = configuration.Url;
            DocumentStore = new Raven.Client.Document.DocumentStore
            {
                Url = configuration.Url
            };

            if (configuration.DefaultDatabase != null)
                DocumentStore.DefaultDatabase = configuration.DefaultDatabase;

            if (configuration.Credentials != null)
                DocumentStore.Credentials = configuration.Credentials;
            

            // TODO : THIS IS NO GOOD!  Working around or camouflaging problems within Bifrost - good thing Raven told me it was a problem.. :) 
            DocumentStore.Conventions.MaxNumberOfRequestsPerSession = 512;
            DocumentStore.Initialize();
        }

        public void Initialize(IContainer container)
        {
            InsertOrModifyEventSourceIdAndVersionIndex();
            
            DocumentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new MethodInfoConverter());
                s.Converters.Add(new EventSourceVersionConverter());
            };
            DocumentStore.Conventions.FindTypeTagName = t =>
            {
                if (t.HasInterface<IEvent>() || t == typeof(IEvent))
                    return "Events";

                return DocumentConvention.DefaultTypeTagName(t);
            };

            if (_configuration.EventsKeyGeneratorType != null)
            {
                var keyGenerator = new SequentialKeyGenerator(DocumentStore);
                var originalDocumentKeyGenerator = DocumentStore.Conventions.DocumentKeyGenerator;
                DocumentStore.Conventions.DocumentKeyGenerator = o =>
                {
                    if (o is IEvent && ((IEvent)o).Id == 0)
                    {
                        var key = keyGenerator.NextFor<IEvent>();
                        return string.Format("{0}/{1}",DocumentStore.Conventions.FindTypeTagName(o.GetType()), key);
                    }
                    return originalDocumentKeyGenerator(o);
                };
            }
        }

        void InsertOrModifyEventSourceIdAndVersionIndex()
        {
            var alreadyExists = true;
            var updated = false;
            var indexName = "Temp/Events/ByEventSourceIdAndVersionSortByVersion";
            var index = DocumentStore.DatabaseCommands.GetIndex(indexName);
            if (index == null)
            {
                index = new IndexDefinition
                {
                    Map = "from doc in docs.Events select new { EventSourceId = doc.EventSourceId, Version = doc.Version }",
                    Fields = new List<string> { "EventSourceId", "Version", "__document_id" }
                };
                alreadyExists = false;
            }

            if (alreadyExists && index.SortOptions.First().Value != SortOptions.Double)
            {
                DocumentStore.DatabaseCommands.DeleteIndex(indexName);
                updated = true;
            }

            index.SortOptions = new Dictionary<string, SortOptions> { { "Version", SortOptions.Double } };

            if( !alreadyExists || updated ) 
                DocumentStore.DatabaseCommands.PutIndex(indexName, index);
        }
    }
}
