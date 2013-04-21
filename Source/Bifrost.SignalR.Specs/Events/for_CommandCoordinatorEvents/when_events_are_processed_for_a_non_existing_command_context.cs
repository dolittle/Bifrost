using System;
using Machine.Specifications;

namespace Bifrost.SignalR.Specs.Events.for_CommandCoordinatorEvents
{
    public class when_events_are_processed_for_a_non_existing_command_context : given.a_command_coordinator_events
    {
        Establish context = () => command_context_connection_manager_mock.Setup(c => c.HasConnectionForCommandContext(Moq.It.IsAny<Guid>())).Returns(false);

        Because of = () => command_coordinator_events.EventsProcessed(Guid.NewGuid());

        It should_not_get_connection_for_command_context = () => command_context_connection_manager_mock.Verify(c => c.GetConnectionForCommandContext(Moq.It.IsAny<Guid>()), Moq.Times.Never());
    }
}
