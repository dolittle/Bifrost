
using Bifrost.Events;
namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class SomeEventSubscriber : IProcessEvents
    {
        public bool ProcessCalled = false;
        public void Process(SomeEvent @event)
        {
            ProcessCalled = true;
        }
    }
}
