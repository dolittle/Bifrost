using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<IEventStore> event_store_mock;
        protected static Mock<ICanSendCommittedEventStream> committed_event_stream_sender_mock;
        protected static Mock<IEventEnvelopes> event_envelopes;

        Establish context = () =>
        {
            event_store_mock = new Mock<IEventStore>();
            committed_event_stream_sender_mock = new Mock<ICanSendCommittedEventStream>();
            event_envelopes = new Mock<IEventEnvelopes>();
        };
    }
}
