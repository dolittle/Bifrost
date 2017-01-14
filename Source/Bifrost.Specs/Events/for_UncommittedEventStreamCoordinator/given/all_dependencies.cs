using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<IEventStore> event_store_mock;
        protected static Mock<ICommittedEventStreamCoordinator> committed_event_stream_coordinator_mock;

        Establish context = () =>
        {
            event_store_mock = new Mock<IEventStore>();
            committed_event_stream_coordinator_mock = new Mock<ICommittedEventStreamCoordinator>();
        };
    }
}
