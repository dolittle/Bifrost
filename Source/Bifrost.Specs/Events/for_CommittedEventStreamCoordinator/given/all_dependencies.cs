using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Events.for_CommittedEventStreamCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<ICanReceiveCommittedEventStream> committed_event_stream_receiver_mock;
        protected static Mock<IEventSubscriptionManager> event_subscription_manager_mock;

        Establish context = () =>
        {
            committed_event_stream_receiver_mock = new Mock<ICanReceiveCommittedEventStream>();
            event_subscription_manager_mock = new Mock<IEventSubscriptionManager>();
        };
    }
}
