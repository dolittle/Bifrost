using System;
using Bifrost.Commands;
using Bifrost.Serialization;
using Bifrost.Web.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Execution;

namespace Bifrost.Web.Specs.Commands.for_CommandCoordinator
{
    public class when_handling_a_command_descriptor
    {
        static Mock<ICommandCoordinator> core_command_coordinator_mock;
        static Mock<ITypeDiscoverer> type_discoverer_mock;
        static Mock<ICommandContextConnectionManager> command_context_connection_manager;
        static Mock<ISerializer> serializer_mock;
        static Bifrost.Web.Commands.CommandCoordinator  command_coordinator;
        static CommandDescriptor command_descriptor;

        Establish context = () =>
        {
            core_command_coordinator_mock = new Mock<ICommandCoordinator>();
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            command_context_connection_manager = new Mock<ICommandContextConnectionManager>();
            serializer_mock = new Mock<ISerializer>();
            command_coordinator = new Web.Commands.CommandCoordinator(
                core_command_coordinator_mock.Object,
                type_discoverer_mock.Object,
                command_context_connection_manager.Object,
                serializer_mock.Object);

            command_descriptor = new CommandDescriptor
            {
                Id = Guid.NewGuid(),
                GeneratedFrom = "SomeServerCommand",
                Name = "Some Command",
                Command = "Actual command"
            };


            type_discoverer_mock.Setup(t => t.FindTypeByFullName(command_descriptor.Name)).Returns(typeof(SimpleCommand));
            serializer_mock.Setup(s => s.FromJson(typeof(SimpleCommand), command_descriptor.Command, null)).Returns(new SimpleCommand());
        };

        Because of = () => command_coordinator.Handle(command_descriptor);

        It should_get_type_from_name = () => type_discoverer_mock.Verify(c => c.FindTypeByFullName(command_descriptor.GeneratedFrom), Times.Once());
    }
}
