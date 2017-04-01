using System;
using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events
{
    public class AnotherSimpleEvent : Event
    {
        public AnotherSimpleEvent(EventSourceId eventSourceId) : base(eventSourceId)
        {}

        public string Content { get; set; }
    }
}