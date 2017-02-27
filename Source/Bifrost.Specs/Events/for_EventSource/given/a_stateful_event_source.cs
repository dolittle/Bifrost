using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSource.given
{
    public class a_stateful_event_source
    {
        protected static StatefulAggregatedRoot event_source;
        protected static Guid event_source_id;
        protected static IEvent @event;

        Establish context =
            () =>
                {
                    event_source_id = Guid.NewGuid();
                    event_source = new StatefulAggregatedRoot(event_source_id);
                };
    }
}