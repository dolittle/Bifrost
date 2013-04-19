using System.Linq;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository.given
{
    public class an_event_subscription_repository_with_two_subscriptions_for_different_event_types : given.an_event_subscription_repository
    {
        Establish context = () =>
        {
            entity_context_mock.Setup(e => e.Entities).Returns(
                new[] {
                    new EventSubscription { EventType = typeof(AnotherSimpleEvent), Owner=typeof(Subscribers), Method=typeof(Subscribers).GetMethod(ProcessMethodInvoker.ProcessMethodName,new[] {typeof(AnotherSimpleEvent)}) },
                    new EventSubscription { EventType = typeof(SimpleEvent), Owner=typeof(Subscribers), Method=typeof(Subscribers).GetMethod(ProcessMethodInvoker.ProcessMethodName,new[] {typeof(AnotherSimpleEvent)}) }
                }.AsQueryable());
        };

    }
}
