using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_creating_instance_for_command
    {
        static CommandBuilder<Command> builder;
        static Mock<ICommandBuildingConventions>    conventions_mock;

        Establish context = () => 
        {
            conventions_mock = new Mock<ICommandBuildingConventions>();
            conventions_mock.Setup(c=>c.CommandName).Returns(n => n);
        };

        Because of = () => builder = new CommandBuilder<Command>(new Mock<ICommandCoordinator>().Object, conventions_mock.Object);

        It should_not_set_any_name = () => builder.Name.ShouldBeNull();
    }
}
