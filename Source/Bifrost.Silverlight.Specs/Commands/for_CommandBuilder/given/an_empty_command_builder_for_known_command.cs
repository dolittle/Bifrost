using Bifrost.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder.given
{
    public class an_empty_command_builder_for_known_command
    {
        protected static Mock<ICommandCoordinator>   command_coordinator_mock;
        protected static CommandBuilder<KnownCommand>  builder;

        Establish context = () =>
        {
            command_coordinator_mock = new Mock<ICommandCoordinator>();
            builder = new CommandBuilder<KnownCommand>(command_coordinator_mock.Object);
        };
    }
}
