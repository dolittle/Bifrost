using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Mimir.Domain.EventSubscriptions.Commands;

namespace Bifrost.Mimir.Domain.EventSubscriptions
{
    public class CommandHandlers : ICommandHandler
    {
        IAggregatedRootRepository<EventSubscription> _repository;
        IEventSubscriptionRepository _eventSubscriptionRepository;

        public CommandHandlers(IAggregatedRootRepository<EventSubscription> repository, IEventSubscriptionRepository eventSubscriptionRepository)
        {
            _repository = repository;
            _eventSubscriptionRepository = eventSubscriptionRepository;
        }


        public void Handle(ReplayAllForEventSubscription command)
        {
            var subscription = _repository.Get(command.EventSubscriptionId);
            subscription.ReplayAll();
        }

        public void Handle(ReplayAllEventSubscriptions command)
        {
            var subscriptions = _eventSubscriptionRepository.GetAll();
            foreach (var subscription in subscriptions)
            {
                var actualSubscription = _repository.Get(subscription.Id);
                actualSubscription.ReplayAll();
            }
        }
    }
}
