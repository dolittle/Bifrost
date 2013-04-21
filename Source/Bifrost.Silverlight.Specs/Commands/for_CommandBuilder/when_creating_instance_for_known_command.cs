using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_creating_instance_for_known_command
    {
        static CommandBuilder<KnownCommand> builder;
        static Mock<ICommandBuildingConventions>    conventions_mock;

        Establish context = () => 
        {
            conventions_mock = new Mock<ICommandBuildingConventions>();
            conventions_mock.Setup(c=>c.CommandName).Returns(n => n);
        };

        Because of = () => builder = new CommandBuilder<KnownCommand>(new Mock<ICommandCoordinator>().Object, conventions_mock.Object);

        It should_set_name_to_be_name_of_command_based_on_convention = () => builder.Name.ShouldEqual("KnownCommand");
    }
}
