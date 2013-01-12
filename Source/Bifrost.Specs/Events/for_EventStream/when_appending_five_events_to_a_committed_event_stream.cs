using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Bifrost.Specs.Events.for_EventStream.behaviors;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream
{
    public class when_appending_five_events_to_a_committed_event_stream : given.an_empty_committed_event_stream
    {
        private static List<IEvent> Events;

        private Establish context =
            () =>
                {
                    Events = new List<IEvent>();
                    var version = EventSourceVersion.Zero.NextCommit();
                    for (var i = 0; i < 5; i++ )
                    {
                        Events.Add(new SimpleEvent(EventSourceId) { Version = version });
                        version = version.NextSequence();
                    } 
                };

        private Because of = () => EventStream.Append(Events);

        private Behaves_like<an_event_stream_with_five_events_appended> an_event_stream;

    }
}