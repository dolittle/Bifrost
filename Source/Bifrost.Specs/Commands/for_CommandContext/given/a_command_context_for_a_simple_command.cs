using Bifrost.Commands;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command
    {
        protected static SimpleCommand command;
        protected static CommandContext command_context;
        protected static Mock<IEventStore>  event_store_mock;
        protected static Mock<IUncommittedEventStreamCoordinator> uncommitted_event_stream_coordinator;

        Establish context = () =>
        {
            command = new SimpleCommand();
            event_store_mock = new Mock<IEventStore>();
            uncommitted_event_stream_coordinator = new Mock<IUncommittedEventStreamCoordinator>();
            command_context = new CommandContext(command, null, event_store_mock.Object, uncommitted_event_stream_coordinator.Object);
        };
    }
}
