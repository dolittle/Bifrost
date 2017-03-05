using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;
using System;
using Bifrost.Events;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command_with_one_tracked_object : a_command_context_for_a_simple_command
    {
        protected static StatefulAggregatedRoot aggregated_root;
        protected static EventSourceId event_source_id;

        Establish context = () =>
        {
            event_source_id = Guid.NewGuid();
            aggregated_root = new StatefulAggregatedRoot(event_source_id);
            aggregated_root.EventEnvelopes = event_envelopes.Object;
            command_context.RegisterForTracking(aggregated_root);
        };
    }
}
