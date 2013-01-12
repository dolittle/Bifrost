using Bifrost.Commands;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_DynamicCommandFactory.given
{
    public class a_dynamic_command_factory
    {
        protected static DynamicCommandFactory factory;

        Establish context = () =>
                                {
                                    StatelessAggregatedRootWithOneMethod.ResetState();
                                    factory = new DynamicCommandFactory();
                                };
    }
}
