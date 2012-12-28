using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Mimir.Domain.EventSubscriptions.Commands;

namespace Bifrost.Mimir.Domain.EventSubscriptions
{
    public class CommandHandlers : ICommandHandler
    {
        IAggregatedRootRepository<EventSubscription> _repository;

        public CommandHandlers(IAggregatedRootRepository<EventSubscription> repository)
        {
            _repository = repository;
        }

        public void Handle(ReplayAllForEventSubscription command)
        {
            var subscription = _repository.Get(command.EventSubscriptionId);
            subscription.ReplayAll();
        }
    }
}
