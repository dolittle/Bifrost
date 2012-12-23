using Bifrost.Events;
using MongoDB.Bson.Serialization;

namespace Bifrost.MongoDB
{
    public class EventClassMap : BsonClassMap<IEvent>
    {
        public EventClassMap()
        {
            AutoMap();
            
        }
    }
}
