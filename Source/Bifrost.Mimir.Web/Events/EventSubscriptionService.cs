using System.Collections;
using System.Linq;
using Bifrost.Events;
using System.Collections.Generic;

namespace Bifrost.Mimir.Web.Events
{
    public class EventSubscriptionService
    {
        IEventSubscriptionRepository _eventSubscriptionRepository;

        public EventSubscriptionService(IEventSubscriptionRepository eventSubscriptionRepository)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
        }

        public IEnumerable<object> GetAll()
        {
            return _eventSubscriptionRepository.GetAll().Select(e => new
            {
                Id = e.Id,
                Owner = e.Owner.Name,
                OwnerNamespace = e.Owner.Namespace,
                OwnerAssembly = e.Owner.Assembly.FullName,
                Event = e.EventName,
                LastEventId = e.LastEventId
            });
        }
    }
}