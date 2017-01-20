/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Web;
using Bifrost.Sagas;

namespace Bifrost.Web.Sagas
{
    public class SagaLibrarian : ISagaLibrarian
    {
        readonly ISagaConverter _sagaConverter;

        public SagaLibrarian(ISagaConverter sagaConverter)
        {
            _sagaConverter = sagaConverter;
        }


        public void Close(ISaga saga)
        {
            HttpContext.Current.Session.Remove(saga.Id.ToString());
        }

        public void Catalogue(ISaga saga)
        {
            var sagaHolder = _sagaConverter.ToSagaHolder(saga);
            HttpContext.Current.Session[saga.Id.ToString()] = sagaHolder;
        }

        public ISaga Get(Guid id)
        {
            var sagaInSession = HttpContext.Current.Session[id.ToString()];
            var exists = sagaInSession != null;
            if (!exists)
                return null;

            var sagaHolder = sagaInSession as SagaHolder;
            var saga = _sagaConverter.ToSaga(sagaHolder);
            return saga;
        }

        public ISaga Get(string partition, string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISaga> GetForPartition(string partition)
        {
            throw new NotImplementedException();
        }
    }
}
