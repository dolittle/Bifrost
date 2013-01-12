using System;
using System.Linq;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_resetting_last_event_id_for_all_subscriptions : given.an_event_subscription_repository
    {
        static Guid first_subscription_id = Guid.NewGuid();
        static Guid second_subscription_id = Guid.NewGuid();
        static EventSubscription first_subscription;
        static EventSubscription second_subscription;

        Establish context = () =>
        {
            first_subscription = new EventSubscription
                {
                    EventType = typeof(SimpleEvent),
                    Owner = typeof(Subscribers),
                    Id = first_subscription_id,
                    LastEventId = 42
                };

            second_subscription = new EventSubscription
                {
                    EventType = typeof(SimpleEvent),
                    Owner = typeof(Subscribers),
                    Id = second_subscription_id,
                    LastEventId = 49
                };

            entity_context_mock.Setup(e => e.Entities).Returns((new[] { first_subscription, second_subscription }).AsQueryable());
        };

        Because of = () => repository.ResetLastEventForAllSubscriptions();

        It should_set_last_event_id_to_zero_for_first_subscription = () => first_subscription.LastEventId = 0;
        It should_set_last_event_id_to_zero_for_second_subscription = () => second_subscription.LastEventId = 0;
        It should_commit = () => entity_context_mock.Verify(e => e.Commit(), Times.Once());
    }
}
