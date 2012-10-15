using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(Guid eventSourceId, long id) : base(eventSourceId,id)
        {
            
        }

        public SimpleEvent(Guid eventSourceId) : this(eventSourceId,0)
        {
        }
    }
}