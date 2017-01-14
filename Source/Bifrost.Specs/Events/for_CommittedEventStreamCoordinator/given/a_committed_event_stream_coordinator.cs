using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_CommittedEventStreamCoordinator.given
{
    public class a_committed_event_stream_coordinator : all_dependencies
    {
        protected static CommittedEventStreamCoordinator committed_event_stream_coordinator;

        Establish context = () => committed_event_stream_coordinator = new CommittedEventStreamCoordinator();
    }
}
