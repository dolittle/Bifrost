using System;
using Bifrost.Specs.Events.for_EventStream.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.behaviors
{
    [Behaviors]
    public class an_empty_event_stream
    {
        protected static EventStream EventStream;

        private It should_have_no_events = () => EventStream.HasEvents.ShouldBeFalse();
        private It should_be_an_empty_collection = () => EventStream.Count.ShouldEqual(0);
    }
}