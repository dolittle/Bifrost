using System;
using Bifrost.Commands;
using Bifrost.Events;
using Bifrost.Fakes.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command
    {
        protected static SimpleCommand command;
        protected static CommandContext command_context;
        protected static Mock<IEventStore>  event_store_mock;

        Establish context = () =>
        {
            command = new SimpleCommand(Guid.NewGuid());
            event_store_mock = new Mock<IEventStore>();
            command_context = new CommandContext(command, null, event_store_mock.Object);
        };
    }
}
