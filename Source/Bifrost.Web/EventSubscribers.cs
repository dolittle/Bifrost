using Bifrost.Events;

namespace Bifrost.Web
{
    public class EventSubscribers : EventSubscriber<StuffToPersist>
    {
        public void Process(StuffDone @event)
        {
            var entity = new StuffToPersist
            {
                Something = @event.Something
            };
            InsertEntity(entity);
        }
    }
}