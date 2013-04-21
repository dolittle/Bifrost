using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;
using System;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command_with_one_tracked_object : a_command_context_for_a_simple_command
    {
        protected static StatefulAggregatedRoot aggregated_root;

        Establish context = () =>
        {
            aggregated_root = new StatefulAggregatedRoot(Guid.NewGuid());
            command_context.RegisterForTracking(aggregated_root);
        };
    }
}
