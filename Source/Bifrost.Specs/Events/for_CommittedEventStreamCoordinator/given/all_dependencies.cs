using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_CommittedEventStreamCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<ICanSendCommittedEventStream> committed_event_stream_sender_mock;

        Establish context = () => committed_event_stream_sender_mock = new Mock<ICanSendCommittedEventStream>();
    }
}
