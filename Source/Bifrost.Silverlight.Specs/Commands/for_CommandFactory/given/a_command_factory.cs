using Bifrost.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory.given
{
    public class a_command_factory
    {
        protected static Mock<ICommandCoordinator> command_coordinator_mock;
        protected static Mock<ICommandBuildingConventions> command_building_conventions;
        protected static CommandFactory command_factory;

        Establish context = () =>
        {
            command_coordinator_mock = new Mock<ICommandCoordinator>();
            command_building_conventions = new Mock<ICommandBuildingConventions>();
            command_building_conventions.SetupGet(c => c.CommandName).Returns(s => s);
            command_factory = new CommandFactory(command_coordinator_mock.Object, command_building_conventions.Object);
        };
    }
}
