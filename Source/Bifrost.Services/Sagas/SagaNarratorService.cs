using System;
using Bifrost.Sagas;

namespace Bifrost.Services.Sagas
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
