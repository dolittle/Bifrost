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
using Bifrost.Entities;
using Bifrost.Execution;
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
            DocumentStore.Conventions.CustomizeJsonSerializer = s =>
            {
                s.Converters.Add(new MethodInfoConverter());
            };
        }
    }
}
