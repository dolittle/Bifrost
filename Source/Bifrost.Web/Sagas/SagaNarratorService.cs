/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Sagas;

namespace Bifrost.Web.Sagas
{
    public class SagaNarratorService
    {
        ISagaLibrarian _sagaLibrarian;
        ISagaNarrator _sagaNarrator;

        public SagaNarratorService(
            ISagaLibrarian sagaLibrarian,
            ISagaNarrator sagaNarrator
            )
        {
            _sagaLibrarian = sagaLibrarian;
            _sagaNarrator = sagaNarrator;
        }

        public SagaConclusion Conclude(Guid sagaId)
        {
            var saga = _sagaLibrarian.Get(sagaId);
            var conclusion = _sagaNarrator.Conclude(saga);
            return conclusion;
        }
    }
}
