using System;
using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class SomeOtherEvent : Event
    {
        public SomeOtherEvent(Guid eventSourceId) : base(eventSourceId) { }
    }
}
