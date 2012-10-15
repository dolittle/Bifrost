using Bifrost.Entities;
using Bifrost.Events;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Bifrost.RavenDB
{
    public class EventsDocumentStoreListener : IDocumentStoreListener
    {
        ISequentialKeyGenerator _keyGenerator;

        public EventsDocumentStoreListener(ISequentialKeyGenerator keyGenerator)
        {
            _keyGenerator = keyGenerator;
        }

        public void AfterStore(string key, object entityInstance, RavenJObject metadata)
        {
            
        }

        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
        {
            if (entityInstance is EventHolder)
            {
                var @event = entityInstance as EventHolder;
                if( @event.Id == 0 )
                    @event.Id = _keyGenerator.NextFor<EventHolder>();
            }
            return true;
        }
    }
}
