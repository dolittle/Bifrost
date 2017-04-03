using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_EventSource.given
{
    public class all_dependencies
    {
        protected static Mock<IEventEnvelopes> event_envelopes;

        Establish context = () => event_envelopes = new Mock<IEventEnvelopes>();
    }
}