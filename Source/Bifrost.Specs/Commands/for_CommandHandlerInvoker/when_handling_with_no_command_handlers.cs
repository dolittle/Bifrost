using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_handling_with_no_command_handlers : given.a_command_handler_invoker_with_no_command_handlers
    {
        protected static bool result;

        Because of = () =>
                         {
                             var command = new Command();
                             result = invoker.TryHandle(command);
                         };

        It should_return_false_when_trying_to_handle = () => result.ShouldBeFalse();
    }
}
