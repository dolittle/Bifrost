using Bifrost.Commands;
using Bifrost.Extensions;
using Machine.Specifications;
using Moq;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder.given
{
    public class an_empty_command_builder_for_known_command_with_constructor_parameter
    {
        protected static Mock<ICommandCoordinator>   command_coordinator_mock;
        protected static Mock<ICommandBuildingConventions> conventions_mock;
        protected static CommandBuilder<KnownCommandWithConstructorParameter>  builder;

        Establish context = () =>
        {
            command_coordinator_mock = new Mock<ICommandCoordinator>();
            conventions_mock = new Mock<ICommandBuildingConventions>();
            conventions_mock.Setup(c => c.CommandName).Returns(n => n);
            conventions_mock.Setup(c => c.CommandConstructorName).Returns(n => n.ToCamelCase());
            builder = new CommandBuilder<KnownCommandWithConstructorParameter>(command_coordinator_mock.Object, conventions_mock.Object);
        };
    }
}
