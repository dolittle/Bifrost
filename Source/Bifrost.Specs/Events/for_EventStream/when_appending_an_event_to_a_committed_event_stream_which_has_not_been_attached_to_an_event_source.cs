using System;
using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream
{
    public class when_appending_an_event_to_a_committed_event_stream_which_has_not_been_attached_to_an_event_source : given.an_empty_committed_event_stream
    {
        static Exception Exception;

        Because of = () =>
                         {
                             var unattachedEvent = new SimpleEvent(Guid.Empty) { Version = new EventSourceVersion(1, 0) }; ;

                             Exception = Catch.Exception(() => EventStream.Append(new List<IEvent>() { unattachedEvent }));
                         };

        It should_throw_an_exception = () => Exception.ShouldNotBeNull();
    }
}