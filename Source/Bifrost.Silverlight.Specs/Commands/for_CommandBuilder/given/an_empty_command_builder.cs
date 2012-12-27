using Bifrost.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder.given
{
    public class an_empty_command_builder
    {
        protected static Mock<ICommandCoordinator>   command_coordinator_mock;
        protected static CommandBuilder<Command>  builder;

        Establish context = () =>
        {
            command_coordinator_mock = new Mock<ICommandCoordinator>();
            builder = new CommandBuilder<Command>(command_coordinator_mock.Object);
        };
    }
}
