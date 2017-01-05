/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.RavenDB.Serialization;
using Raven.Client.Document;

namespace Bifrost.RavenDB
{
    public class EntityContextConnection : IEntityContextConnection
    {
        public DocumentStore DocumentStore { get; private set; }

        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            DocumentStore = configuration.CreateDocumentStore();

            var originalFindIdentityProperty = DocumentStore.Conventions.FindIdentityProperty;
            DocumentStore.Conventions.FindIdentityProperty = prop => configuration.IdPropertyRegister.IsIdProperty(prop.DeclaringType, prop) || originalFindIdentityProperty(prop);
            DocumentStore.Conventions.IdentityTypeConvertors.AddRange(configuration.IdPropertyRegister.GetTypeConvertersForConceptIds());

            // TODO : THIS IS NO GOOD!  Working around or camouflaging problems within Bifrost - good thing Raven told me it was a problem.. :) 
            DocumentStore.Conventions.MaxNumberOfRequestsPerSession = 4096;
        }

        public void Initialize(IContainer container)
        {
            DocumentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new MethodInfoConverter());
                s.Converters.Add(new ConceptConverter());
                s.Converters.Add(new ConceptDictionaryConverter());
            };

            DocumentStore.Conventions.AllowQueriesOnId = true;
            //DocumentStore.Conventions.IdentityTypeConvertors.Add(new ConceptTypeConverter());
        }
    }
}
