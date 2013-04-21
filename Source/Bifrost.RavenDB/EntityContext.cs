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
using System.Linq;
using Bifrost.Entities;
using Raven.Abstractions.Commands;
using Raven.Client;

namespace Bifrost.RavenDB
{
    public class EntityContext<T> : IEntityContext<T>
    {
        IEntityContextConnection _connection;
        
        IDocumentSession _session;
        string _indexName;

        public EntityContext(EntityContextConnection connection)
        {
            _connection = connection;
            _session = connection.DocumentStore.OpenSession();
            _indexName = connection.DocumentStore.Conventions.FindTypeTagName(typeof(T));
        }


        public IQueryable<T> Entities { get { return _session.Query<T>(); } }


        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
            _session.Store(entity);
        }

        public void Update(T entity)
        {
            _session.Store(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public void Save(T entity)
        {
            _session.Store(entity);
            _session.SaveChanges();
        }

        public void Commit()
        {
            _session.SaveChanges();
        }

        public void Dispose()
        {
            _session.SaveChanges();
            _session.Dispose();
        }


        public T GetById<TProperty>(TProperty id)
        {
            var keyId = GetDocumentIdForType<TProperty>(id);
            return _session.Load<T>(keyId);
        }

        public void DeleteById<TProperty>(TProperty id)
        {
            var keyId = GetDocumentIdForType<TProperty>(id);
            _session.Advanced.DocumentStore.DatabaseCommands.Delete(keyId, null);
        }


        string GetDocumentIdForType<TProperty>(TProperty id)
        {
            var documentKeyName = GetDocumentKeyForType();

            var keyId = string.Format("{0}/{1}", documentKeyName, id);
            return keyId;
        } 

        string GetDocumentKeyForType()
        {
            return _session.Advanced.DocumentStore.Conventions.GetTypeTagName(typeof(T));
        }
    }
}
