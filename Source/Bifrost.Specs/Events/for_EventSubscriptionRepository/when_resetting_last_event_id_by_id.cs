using System;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class when_resetting_last_event_id_by_id : given.an_event_subscription_repository
    {
        static Guid subscription_id = Guid.NewGuid();
        static EventSubscription subscription;
        static EventSubscription updated_subscription;

        Establish context = () =>
        {
            entity_context_mock.Setup(e => e.Entities).Returns((new[] { 
                new EventSubscription
                { 
                    EventType = typeof(SimpleEvent),
                    Owner = typeof(Subscribers),
                    Id = subscription_id,
                    LastEventId = 42
                }
            }).AsQueryable());

            entity_context_mock.Setup(e => e.Update(Moq.It.IsAny<EventSubscription>())).Callback((EventSubscription e) => updated_subscription = e);
        };

        Because of = () => repository.ResetLastEventId(subscription_id);

        It should_set_last_event_id_to_zero = () => updated_subscription.LastEventId = 0;
        It should_commit = () => entity_context_mock.Verify(e => e.Commit(), Times.Once());
    }
}
