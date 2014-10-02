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
using System.Linq;

namespace Bifrost.Entities
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEntityContext"/> doing absolutely nothing
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public class NullEntityContext<T> : IEntityContext<T>
    {
#pragma warning disable 1591 // Xml Comments
        public IQueryable<T> Entities
        {
            get { return new T[0].AsQueryable(); }
        }

        public void Attach(T entity)
        {
        }

        public void Insert(T entity)
        {
        }

        public void Update(T entity)
        {
        }

        public void Delete(T entity)
        {
        }

        public void Save(T entity)
        {
        }

        public void Commit()
        {
        }

        public T GetById<TProperty>(TProperty id)
        {
            return default(T);
        }

        public void DeleteById<TProperty>(TProperty id)
        {
        }

        public void Dispose()
        {
        }
#pragma warning restore 1591 // Xml Comments
    }
}
