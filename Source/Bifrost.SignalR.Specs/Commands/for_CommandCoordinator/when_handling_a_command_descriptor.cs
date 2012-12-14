using System;
using Bifrost.Commands;
using Bifrost.Serialization;
using Bifrost.SignalR.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Fakes.Commands;

namespace Bifrost.SignalR.Specs.Commands.for_CommandCoordinator
{
    public class when_handling_a_command_descriptor
    {
        static Mock<ICommandCoordinator> core_command_coordinator_mock;
        static Mock<ICommandTypeManager> command_type_manager_mock;
        static Mock<ICommandContextConnectionManager> command_context_connection_manager;
        static Mock<ISerializer> serializer_mock;
        static Bifrost.SignalR.Commands.CommandCoordinator  command_coordinator;
        static CommandDescriptor command_descriptor;

        Establish context = () =>
        {
            core_command_coordinator_mock = new Mock<ICommandCoordinator>();
            command_type_manager_mock = new Mock<ICommandTypeManager>();
            command_context_connection_manager = new Mock<ICommandContextConnectionManager>();
            serializer_mock = new Mock<ISerializer>();
            command_coordinator = new SignalR.Commands.CommandCoordinator(
                core_command_coordinator_mock.Object,
                command_type_manager_mock.Object,
                command_context_connection_manager.Object,
                serializer_mock.Object);

            command_descriptor = new CommandDescriptor
            {
                Id = Guid.NewGuid(),
                Name = "Something",
                Command = "Actual command"
            };

            command_type_manager_mock.Setup(c => c.GetFromName(command_descriptor.Name)).Returns(typeof(SimpleCommand));
            serializer_mock.Setup(s => s.FromJson(typeof(SimpleCommand), command_descriptor.Command, null)).Returns(new SimpleCommand());
        };

        Because of = () => command_coordinator.Handle(command_descriptor);

        It should_get_type_from_name = () => command_type_manager_mock.Verify(c => c.GetFromName(command_descriptor.Name), Times.Once());
    }
}
