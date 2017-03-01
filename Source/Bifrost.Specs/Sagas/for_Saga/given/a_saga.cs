using Bifrost.Events;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga
    {
        protected static Saga saga;
        protected static Mock<IEventEnvelopes> event_envelopes;

        Establish context = () =>
        {
            event_envelopes = new Mock<IEventEnvelopes>();
            saga = new Saga();
            saga.EventEnvelopes = event_envelopes.Object;
        };
    }
}
