using Bifrost.Fakes.Events;

namespace Bifrost.Mimir.Views.Specs.EventSubscriptions.for_EventSubscribers
{
    public class Subscribers
    {
        public void Process(SimpleEvent @event)
        {
        }

        public void Process(AnotherSimpleEvent @event)
        {
        }
    }
}
