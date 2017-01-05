/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
