#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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

using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Entities;

namespace Bifrost.Fakes.Entities
{
    public class EntityContext<T> : IEntityContext<T>
    {
        private readonly List<T> _entities;
    	readonly List<T> _entitiesToDelete;

        public EntityContext()
        {
            _entities = new List<T>();
			_entitiesToDelete = new List<T>();
        }

        public EntityContext(IEnumerable<T> entities) : this()
        {
           Populate(entities);
        }

        public void Dispose()
        {}

        public IQueryable<T> Entities
        {
            get { return _entities.AsQueryable(); }
        }

        public void Attach(T entity)
        {
            _entities.Add(entity);
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {}

        public void Delete(T entity)
        {
			_entitiesToDelete.Add(entity);
        }

        public void Save(T entity)
        {
            
        }

        public bool CommitCalled = false;
        public void Commit()
        {
			foreach (var entity in _entitiesToDelete)
				_entities.Remove(entity);

        	_entitiesToDelete.Clear();
            CommitCalled = true;
        }

        public void Populate(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }
    }
}