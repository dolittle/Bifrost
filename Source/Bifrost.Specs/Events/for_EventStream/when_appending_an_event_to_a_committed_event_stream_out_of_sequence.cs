using System;
using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream
{
    public class when_appending_an_event_to_a_committed_event_stream_out_of_sequence : given.an_empty_committed_event_stream
    {
        static Exception Exception;

        Because of = () =>
                         {
                             var firstEvent = new SimpleEvent(EventSourceId) { Version = new EventSourceVersion(1, 0) };
                             var eventOutOfSequence = new SimpleEvent(EventSourceId) { Version = new EventSourceVersion(1, 2) };

                             Exception = Catch.Exception(() => EventStream.Append(new List<IEvent>() { firstEvent, eventOutOfSequence }));
                         };

        It should_throw_an_event_out_of_sequence_exception = () => Exception.ShouldBeOfType<EventOutOfSequenceException>();
    }
}