using System;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream.given
{
    public abstract class an_empty_committed_event_stream
    {
        protected static CommittedEventStream EventStream;
        protected static Guid EventSourceId;

        Establish context = () =>
                {
                    EventSourceId = Guid.NewGuid();
                    EventStream = new CommittedEventStream(EventSourceId);
                };
    }
}