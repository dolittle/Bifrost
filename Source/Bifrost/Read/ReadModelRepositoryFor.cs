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
using System.Linq;
using Bifrost.Entities;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IReadModelRepositoryFor{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of ReadModel the repository represents</typeparam>
    public class ReadModelRepositoryFor<T> : IReadModelRepositoryFor<T> where T:IReadModel
    {
        IEntityContext<T> _entityContext;

        /// <summary>
        /// Initializes a new instance of <see cref="ReadModelRepositoryFor{T}"/>
        /// </summary>
        /// <param name="entityContext"><see cref="IEntityContext{T}"/> used by the repository</param>
        public ReadModelRepositoryFor(IEntityContext<T> entityContext)
        {
            _entityContext = entityContext;
        }

#pragma warning disable 1591
        public IQueryable<T> Query { get { return _entityContext.Entities; } }

        public void Insert(T readModel)
        {
            _entityContext.Insert(readModel);
            _entityContext.Commit();
        }

        public void Update(T readModel)
        {
            _entityContext.Update(readModel);
            _entityContext.Commit();
        }

        public void Delete(T readModel)
        {
            _entityContext.Delete(readModel);
            _entityContext.Commit();
        }

        public T GetById(object id)
        {
            return _entityContext.GetById(id);
        }
#pragma warning restore 1591
    }
}
