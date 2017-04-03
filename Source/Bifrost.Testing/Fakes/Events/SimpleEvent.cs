using System;
using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events
{
    public class SimpleEvent : Event
    {
        public SimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {
            
        }

        public string Content { get; set; }
    }
}