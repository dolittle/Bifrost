using System;
using System.Transactions;
using Bifrost.Configuration;
using Bifrost.Entities;
using Raven.Abstractions.Data;
using Raven.Abstractions.Exceptions;
using Raven.Client.Document;
using Raven.Json.Linq;

namespace Bifrost.RavenDB
{
    public class SequentialKeyGenerator : ISequentialKeyGenerator
    {
        const string CurrentPropertyName = "Current";

        static class GeneratorLock<T>
        {
            public static readonly object LockObject = new object();
        }

        DocumentStore _documentStore;

        public SequentialKeyGenerator(IEntityContextConfiguration configuration)
        {
            _documentStore = ((EntityContextConnection)((EntityContextConfiguration)configuration).Connection).DocumentStore;
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
            return string.Format("SequentialKeys/{0}", typeof(T).Name);
        }

    }
}
