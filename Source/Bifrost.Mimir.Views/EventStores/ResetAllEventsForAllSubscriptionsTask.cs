using Bifrost.Tasks;
using System.Linq;
using System.Threading;
using Bifrost.Events;
using Bifrost.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Mimir.Views.EventStores
{
    public class ResetAllEventsForAllSubscriptionsTask : Task
    {
        public override TaskOperation[] Operations
        {
            get { return new TaskOperation[] { Perform }; }
        }

        public int PageNumber { get; set; }

        IEventSubscriptionManager _eventSubscriptionManager;
        IEventStore _eventStore;

        public ResetAllEventsForAllSubscriptionsTask(
                IEventSubscriptionManager eventSubcriptionManager,
                IEventStore eventStore
            )
        {
            _eventSubscriptionManager = eventSubcriptionManager;
            _eventStore = eventStore;
        }


        void Perform(Task task, int operationIndex)
        {
            IEnumerable<IEvent> events;
            do
            {
                events = _eventStore.GetBatch(PageNumber, 10);
                if (events.Count() <= 0)
                    break;

                var actualEvents = events.Where(e => !e.GetType().Namespace.Contains("Mimir"));
                _eventSubscriptionManager.Process(actualEvents);
                PageNumber++;
                Progress();
            } while (events.Count() > 0 && events != null);
        }
    }
}
