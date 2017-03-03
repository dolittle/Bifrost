using Bifrost.Commands;
using Machine.Specifications;
using It = Moq.It;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker.given
{
    public class a_command_handler_invoker_with_one_command_handler : a_command_handler_invoker_with_no_command_handlers
    {
        protected static CommandHandler handler;

        Establish context = () =>
                                {
                                    handler = new CommandHandler();
                                    type_discoverer_mock.Setup(t => t.FindMultiple<IHandleCommands>()).Returns(new[]
                                                                                                              {typeof(CommandHandler)});

                                    container_mock.Setup(c => c.Get(typeof (CommandHandler))).Returns(handler);
                                };
    }
}