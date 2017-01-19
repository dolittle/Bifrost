using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_automatically_discovered_command_handlers : given.a_command_handler_invoker_with_one_command_handler
    {
        static bool result;

        Because of = () =>
                         {
                             var command = new Command();
                             result = invoker.TryHandle(command);
                         };

        It should_return_true_when_trying_to_handle = () => result.ShouldBeTrue();
    }
}