using Bifrost.Events;
using System;

namespace Bifrost.Specs.Events.for_EventSubscriptionManager
{
    public class SomeEvent : Event
    {
        public SomeEvent(Guid eventSourceId) : base(eventSourceId) { }
    }
}
