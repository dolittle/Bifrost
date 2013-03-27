#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
            DocumentStore.Conventions.MaxNumberOfRequestsPerSession = 512;
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
