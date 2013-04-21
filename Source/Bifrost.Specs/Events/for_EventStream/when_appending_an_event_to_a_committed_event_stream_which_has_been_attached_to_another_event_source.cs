using System;
using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream
{
    public class when_appending_an_event_to_a_committed_event_stream_which_has_been_attached_to_another_event_source : given.an_empty_committed_event_stream
    {
        static Exception Exception;

        Because of = () =>
                         {
                             var eventForOtherSource = new SimpleEvent(Guid.NewGuid()) { Version = new EventSourceVersion(1, 0) }; ;
                             Exception = Catch.Exception(() => EventStream.Append(new List<IEvent>() { eventForOtherSource }));
                         };

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();
    }
}