using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class EventSubscriberForTwoEvents : IProcessEvents
    {
        public void Process(SomeEvent @event)
        {
        }

        public void Process(SomeOtherEvent @event)
        {
        }
    }
}
