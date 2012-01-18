using Bifrost.Specs.Events.for_EventStream.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream.behaviors
{
    [Behaviors]
    public class an_event_stream_with_five_events_appended
    {
        protected static EventStream EventStream;

        private It should_have_events = () => EventStream.HasEvents.ShouldBeTrue();
        private It should_have_an_event_count_of_5 = () => EventStream.Count.ShouldEqual(5);
    }
}