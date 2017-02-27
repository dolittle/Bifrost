using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_manually_registered_command_handlers : given.a_command_handler_invoker_with_no_command_handlers
    {
        static CommandHandler handler;
        static bool result;

        Establish context = () =>
                                {
                                    handler = new CommandHandler();
                                    container_mock.Setup(c => c.Get(typeof (CommandHandler))).Returns(
                                        handler);

                                    invoker.Register(typeof (CommandHandler));
                                };

        Because of = () =>
                        {
                            var command = new Command();
                            result = invoker.TryHandle(command);
                        };

        It should_return_true_when_trying_to_handle = () => result.ShouldBeTrue();
    }
}
