using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_UncommittedEventStream.given
{
    public abstract class an_empty_uncommitted_event_stream
    {
        protected static UncommittedEventStream event_stream;
        protected static EventSourceId event_source_id;

        Establish context = () =>
                {
                    event_source_id = Guid.NewGuid();
                    event_stream = new UncommittedEventStream(event_source_id);
                };
    }
}