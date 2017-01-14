using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_UncommittedEventStreamCoordinator.given
{
    public class an_uncommitted_event_stream_coordinator : all_dependencies
    {
        protected static UncommittedEventStreamCoordinator coordinator;

        Establish context = () => coordinator = new UncommittedEventStreamCoordinator(event_store_mock.Object, committed_event_stream_coordinator_mock.Object);
    }
}
