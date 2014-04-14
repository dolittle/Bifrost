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
using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.MongoDB.Concepts;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bifrost.MongoDB
{
    public class EntityContextConnection : IEntityContextConnection
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public MongoServer Server { get; private set; }
        public MongoDatabase Database { get; private set; }

        public EntityContextConnection(EntityContextConfiguration configuration)
        {
            ConnectionString = configuration.Url;
            DatabaseName = configuration.DefaultDatabase;

            Server = MongoServer.Create(ConnectionString);
            Database = Server.GetDatabase(DatabaseName);
            BsonSerializer.RegisterSerializationProvider(new ConceptSerializationProvider());
        }

        public void Initialize(IContainer container)
        {
            
        }
    }
}
