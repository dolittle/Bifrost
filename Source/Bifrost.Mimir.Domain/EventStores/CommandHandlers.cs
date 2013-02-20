using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Mimir.Domain.EventStores.Commands;

namespace Bifrost.Mimir.Domain.EventStores
{
    public class CommandHandlers : IHandleCommands
    {
        IAggregatedRootRepository<EventStore> _repository;

        public CommandHandlers(IAggregatedRootRepository<EventStore> repository)
        {
            _repository = repository;
        }

        public void Handle(ReplayAll command)
        {
            var eventStore = _repository.Get(EventStore.SystemEventStoreId);
            eventStore.ReplayAll();
        }
    }
}
