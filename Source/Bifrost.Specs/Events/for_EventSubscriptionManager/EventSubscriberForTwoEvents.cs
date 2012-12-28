using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class EventSubscriberForTwoEvents : IEventSubscriber
    {
        public void Process(SomeEvent @event)
        {
        }

        public void Process(SomeOtherEvent @event)
        {
        }
    }
}
