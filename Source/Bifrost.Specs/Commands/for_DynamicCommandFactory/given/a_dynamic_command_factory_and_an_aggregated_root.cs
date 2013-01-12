using System;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_DynamicCommandFactory.given
{
    public class a_dynamic_command_factory_and_an_aggregated_root : a_dynamic_command_factory
    {
        protected static StatelessAggregatedRootWithOneMethod aggregated_root;

        Establish context = () => aggregated_root = new StatelessAggregatedRootWithOneMethod(Guid.NewGuid());
    }
}