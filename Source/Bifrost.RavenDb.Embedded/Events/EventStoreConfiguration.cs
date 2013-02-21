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
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace Bifrost.RavenDB.Embedded.Events
{
    public class EventStoreConfiguration : Bifrost.RavenDB.Events.EventStoreConfiguration
    {
        public string DataDirectory { get; set; }
        public bool EnableManagementStudio { get; set; }

        public override DocumentStore CreateDocumentStore()
        {
            var documentStore = DocumentStores.GetAndInitializeByPath(DataDirectory, EnableManagementStudio);

            if (DefaultDatabase != null)
                documentStore.DefaultDatabase = DefaultDatabase;

            if (Credentials != null)
                documentStore.Credentials = Credentials;

            return documentStore;
        }
    }
}
