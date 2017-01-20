using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextManager
{
    [Subject(Subjects.getting_context)]
    public class when_getting_current : given.a_command_context_manager
    {
        static ICommandContext commandContext;
        static ICommandContext currentContext;
        static ICommand command;

        Establish context = () =>
                                {
                                    command = new SimpleCommand();
                                    commandContext = Manager.EstablishForCommand(command);
                                };

        Because of = () => currentContext = Manager.GetCurrent();

        It should_not_return_null = () => currentContext.ShouldNotBeNull();
        It should_return_same_context_as_when_calling_it_for_a_command = () => currentContext.ShouldEqual(commandContext);
        It should_return_context_with_command_in_it = () => currentContext.Command.ShouldEqual(command);
        It should_return_same_when_calling_it_twice_on_same_thread = () =>
                                                                         {
                                                                             var secondContext = Manager.GetCurrent();
                                                                             secondContext.ShouldEqual(currentContext);
                                                                         };
    }
}