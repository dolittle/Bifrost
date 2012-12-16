using Machine.Specifications;
using Bifrost.Events;
using Moq;
using Bifrost.Entities;
using Bifrost.Serialization;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository.given
{
    public class an_event_subscription_repository
    {
        protected static Mock<IEntityContext<EventSubscription>> entity_context_mock;
        protected static EventSubscriptionRepository repository;

        Establish context = () =>
        {
            entity_context_mock = new Mock<IEntityContext<EventSubscription>>();
            repository = new EventSubscriptionRepository(entity_context_mock.Object);
        };
    }
}
