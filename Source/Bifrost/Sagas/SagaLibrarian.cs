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


using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Entities;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ISagaLibrarian"/>
    /// </summary>
	public class SagaLibrarian : ISagaLibrarian
	{
		readonly IEntityContext<SagaHolder> _entityContext;
		readonly ISagaConverter _sagaConverter;

        /// <summary>
        /// Initializes a new instance of <see cref="SagaLibrarian"/>
        /// </summary>
        /// <param name="entityContext">A <see cref="IEntityContext{SagaHolder}"/> to use for working with persisting and resuming <see cref="ISaga">Sagas</see></param>
        /// <param name="sagaConverter">A <see cref="ISagaConverter"/> for converting a <see cref="ISaga"/> to a <see cref="SagaHolder"/> and back</param>
		public SagaLibrarian(IEntityContext<SagaHolder> entityContext, ISagaConverter sagaConverter)
		{
			_entityContext = entityContext;
			_sagaConverter = sagaConverter;
		}

#pragma warning disable 1591 // Xml Comments
        public void Close(ISaga saga)
		{
			var sagaHolder = _sagaConverter.ToSagaHolder(saga);
			_entityContext.Delete(sagaHolder);
			_entityContext.Commit();
		}

		public void Catalogue(ISaga saga)
		{
			var sagaHolder = GetExistingIfAny(saga.Id);
			if (sagaHolder == null)
			{
				sagaHolder = _sagaConverter.ToSagaHolder(saga);
				_entityContext.Insert(sagaHolder);
			}
			else
			{
				_sagaConverter.Populate(sagaHolder, saga);
				_entityContext.Update(sagaHolder);
			}
			_entityContext.Commit();
		}

		public ISaga Get(Guid id)
		{
			var sagaHolder = (from s in _entityContext.Entities
								  where s.Id == id
								  select s).SingleOrDefault();
			var saga = _sagaConverter.ToSaga(sagaHolder);
			return saga;
		}


		public ISaga Get(string partition, string key)
		{
			var query = from s in _entityContext.Entities
			            where s.Partition == partition && s.Key == key
			            select _sagaConverter.ToSaga(s);

			return query.SingleOrDefault();
		}

		public IEnumerable<ISaga> GetForPartition(string partition)
		{
			var sagaHolders = (from s in _entityContext.Entities
			                      where s.Partition == partition
			                      select s).ToArray();
			var sagas = sagaHolders.Select(_sagaConverter.ToSaga);
			return sagas;
		}
#pragma warning restore 1591 // Xml Comments


        SagaHolder GetExistingIfAny(Guid id)
		{
			var sagaHolder = (from s in _entityContext.Entities
								  where s.Id == id
								  select s).SingleOrDefault();
			return sagaHolder;
		}
	}
}
