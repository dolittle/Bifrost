using System;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository.given
{
    public class an_event_subscription_repository_with_one_subscription : given.an_event_subscription_repository
    {
        protected static EventSubscriptionHolder subscription_holder = new EventSubscriptionHolder
        {
            Id = Guid.NewGuid(),
            LastEventId = 42,
            EventName = "SimpleEvent",
            EventType = typeof(SimpleEvent).AssemblyQualifiedName,
            Owner = typeof(Subscribers).AssemblyQualifiedName,
            Method = ProcessMethodInvoker.ProcessMethodName
        };

        Establish context = () => entity_context_mock.Setup(e => e.Entities).Returns(new[] { subscription_holder }.AsQueryable());
    }
}
