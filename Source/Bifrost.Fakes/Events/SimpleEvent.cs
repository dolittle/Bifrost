using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(Guid eventSourceId, Guid id) : base(eventSourceId,id)
        {
            
        }

        public SimpleEvent(Guid eventSourceId) : this(eventSourceId,Guid.NewGuid())
        {
        }
    }
}