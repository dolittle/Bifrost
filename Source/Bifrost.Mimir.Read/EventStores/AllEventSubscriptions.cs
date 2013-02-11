using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Events;
using Bifrost.Read;

namespace Bifrost.Mimir.Views.EventStores
{
    public class AllEventSubscriptions : IQueryFor<EventSubscription>
    {
        IEventSubscriptionRepository _eventSubscriptionRepository;

        public AllEventSubscriptions(IEventSubscriptionRepository eventSubscriptionRepository)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
        }

        public IQueryable<EventSubscription> Query
        {
            get 
            {
                return _eventSubscriptionRepository.GetAll().Select(e => new EventSubscription
                        {
                            Id = e.Id,
                            Owner = e.Owner.Name,
                            OwnerNamespace = e.Owner.Namespace,
                            OwnerAssembly = e.Owner.Assembly.FullName,
                            Event = e.EventName,
                            LastEventId = e.LastEventId
                        }).AsQueryable();
            }
        }
    }
}
