using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class AnotherSimpleEvent : Event
    {
        public AnotherSimpleEvent(Guid eventSourceId, Guid id) : base(eventSourceId,id)
        {}

        public AnotherSimpleEvent(Guid eventSourceId) : this(eventSourceId, Guid.NewGuid())
        {}
    }
}