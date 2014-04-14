#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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

using System.Collections.Generic;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Server;
namespace Bifrost.RavenDB.Embedded
{
    public class DocumentStores
    {
        static Dictionary<string, DocumentStore> _documentStores = new Dictionary<string, DocumentStore>();

        public static DocumentStore GetAndInitializeByPath(string path, bool enableManagementStudio, int managementStudioPort)
        {
            if (!_documentStores.ContainsKey(path))
            {
                if( enableManagementStudio )
                    NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(managementStudioPort);

                var documentStore = new EmbeddableDocumentStore 
                { 
                    DataDirectory = path,
                };

                if (enableManagementStudio)
                    documentStore.UseEmbeddedHttpServer = true;
                
                documentStore.Conventions.CustomizeJsonSerializer = s =>
                {
                    s.Converters.Add(new MethodInfoConverter());
                };

                documentStore.Initialize();

                _documentStores[path] = documentStore;
            }
            return _documentStores[path];
        }
    }
}
