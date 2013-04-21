using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Bifrost.Specs.Events.behaviors;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream
{
    public class when_appending_an_event_to_a_committed_event_stream : given.an_empty_committed_event_stream
    {
        static IEvent Event;

        Establish context =
            () =>
                {
                    Event = new SimpleEvent(EventSourceId) { Version = new EventSourceVersion(1, 0) }; 
                };

        Because of = () => EventStream.Append(new List<IEvent>() { Event });

        Behaves_like<an_event_stream_with_one_event_appended> an_event_stream;

    }
}