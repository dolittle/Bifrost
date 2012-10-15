using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class AnotherSimpleEvent : Event
    {
        public AnotherSimpleEvent(Guid eventSourceId, long id) : base(eventSourceId,id)
        {}

        public AnotherSimpleEvent(Guid eventSourceId) : this(eventSourceId, 0)
        {}
    }
}