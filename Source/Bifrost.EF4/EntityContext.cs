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
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Bifrost.Entities;

namespace Bifrost.EF4
{
	public class EntityContextConfiguration
	{
		public string ConnectionString { get; set; }
	}


	public class EntityContext<T> : IEntityContext<T>
	{
		public EntityContext(EntityContextConfiguration configuration)
		{
            
			//var builder = new ModelBuilder();
		}


		public IQueryable<T> Entities
		{
			get { throw new NotImplementedException(); }
		}

		public void Attach(T entity)
		{
			throw new NotImplementedException();
		}

		public void Insert(T entity)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(T entity)
		{
			throw new NotImplementedException();
		}

	    public void Save(T entity)
	    {
	        throw new NotImplementedException();
	    }

	    public void Commit()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
