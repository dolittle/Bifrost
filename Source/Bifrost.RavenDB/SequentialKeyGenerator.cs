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
using System.Transactions;
using Raven.Abstractions.Data;
using Raven.Abstractions.Exceptions;
using Raven.Client.Document;
using Raven.Json.Linq;

namespace Bifrost.RavenDB
{
    public class SequentialKeyGenerator
    {
        const string CurrentPropertyName = "Current";

        static class GeneratorLock<T>
        {
            public static readonly object LockObject = new object();
        }

        DocumentStore _documentStore;

        public SequentialKeyGenerator(DocumentStore documentStore)
        {
            _documentStore = documentStore;
        }
        

        public long NextFor<T>()
        {
            lock (GeneratorLock<T>.LockObject)
            {
                using (new TransactionScope(TransactionScopeOption.Suppress))
                {
                    while (true)
                    {
                        try
                        {
                            var document = GetDocument<T>();
                            if (document == null)
                            {
                                PutDocument<T>(new JsonDocument
                                {
                                    Etag = Guid.Empty, 
                                    Metadata = new RavenJObject(),
                                    DataAsJson = RavenJObject.FromObject(new { Current = 1 }),
                                    Key = GetDocumentPathFor<T>()
                                });
                                return 1;
                            }

                            var current = 0L;
                            current = document.DataAsJson.Value<long>(CurrentPropertyName);
                            current++;

                            document.DataAsJson[CurrentPropertyName] = current;
                            PutDocument<T>(document);

                            return current;
                        }
                        catch (ConcurrencyException)
                        {
                            // expected, retry
                        }
                    }
                }
            }
        }

        void PutDocument<T>(JsonDocument document)
        {
            _documentStore.DatabaseCommands.Put(
                GetDocumentPathFor<T>(),
                document.Etag,
                document.DataAsJson,
                document.Metadata);
        }

        JsonDocument GetDocument<T>()
        {
            return _documentStore.DatabaseCommands.Get(GetDocumentPathFor<T>());
        }

        string GetDocumentPathFor<T>()
        {
            return string.Format("SequentialKeys/{0}", _documentStore.Conventions.FindTypeTagName(typeof(T)));
        }
    }
}
