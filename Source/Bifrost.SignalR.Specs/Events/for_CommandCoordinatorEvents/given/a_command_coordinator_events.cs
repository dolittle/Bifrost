using Bifrost.SignalR.Commands;
using Bifrost.SignalR.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.SignalR.Specs.Events.for_CommandCoordinatorEvents.given
{
    public class a_command_coordinator_events
    {
        protected static Mock<ICommandContextConnectionManager> command_context_connection_manager_mock;
        protected static CommandCoordinatorEvents command_coordinator_events;

        Establish context = () =>
        {
            command_context_connection_manager_mock = new Mock<ICommandContextConnectionManager>();
            command_coordinator_events = new CommandCoordinatorEvents(command_context_connection_manager_mock.Object);
        };
    }
}
