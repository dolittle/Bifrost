using System;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository.given
{
    public class an_event_subscription_repository_with_one_subscription : given.an_event_subscription_repository
    {
        protected static EventSubscription subscription_holder = new EventSubscription
        {
            Id = Guid.NewGuid(),
            LastEventId = 42,
            EventName = "SimpleEvent",
            EventType = typeof(SimpleEvent),
            Owner = typeof(Subscribers),
            Method = typeof(Subscribers).GetMethod(ProcessMethodInvoker.ProcessMethodName,new Type[] { typeof(SimpleEvent) })
        };

        Establish context = () => entity_context_mock.Setup(e => e.Entities).Returns(new[] { subscription_holder }.AsQueryable());
    }
}
