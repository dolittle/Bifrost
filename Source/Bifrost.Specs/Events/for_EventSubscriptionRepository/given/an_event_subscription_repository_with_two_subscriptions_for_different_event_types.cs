using System.Linq;
using Machine.Specifications;
using Bifrost.Events;
using Bifrost.Fakes.Events;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository.given
{
    public class an_event_subscription_repository_with_two_subscriptions_for_different_event_types : given.an_event_subscription_repository
    {
        Establish context = () =>
        {
            entity_context_mock.Setup(e => e.Entities).Returns(
                new[] {
                    new EventSubscriptionHolder { EventType = typeof(AnotherSimpleEvent).AssemblyQualifiedName, Owner=typeof(Subscribers).AssemblyQualifiedName, Method=ProcessMethodInvoker.ProcessMethodName },
                    new EventSubscriptionHolder { EventType = typeof(SimpleEvent).AssemblyQualifiedName, Owner=typeof(Subscribers).AssemblyQualifiedName, Method=ProcessMethodInvoker.ProcessMethodName }
                }.AsQueryable());
        };

    }
}
