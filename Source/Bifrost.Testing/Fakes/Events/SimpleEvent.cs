using System;
using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(Guid eventSourceId, long id) : base(eventSourceId,id)
        {
            
        }

        public SimpleEvent(Guid eventSourceId) : this(eventSourceId,0)
        {
        }


        public string Content { get; set; }
    }
}