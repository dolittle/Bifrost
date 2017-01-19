using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextManager
{
    public class SimpleCommand : ICommand
    {
        public Guid Id { get; set; }
    }

    [Subject(Subjects.establishing_context)]
    public class when_establishing_for_same_command : given.a_command_context_manager
    {
        static ICommandContext commandContext;
        static ICommand command;

        Because of = () =>
                         {
                             command = new SimpleCommand();
                             commandContext = Manager.EstablishForCommand(command);
                         };

        It should_return_a_non_null_context = () => commandContext.ShouldNotBeNull();
        It should_return_context_with_command_in_it = () => commandContext.Command.ShouldEqual(command);
        It should_return_the_same_calling_it_twice_on_same_thread = () =>
                                                                        {
                                                                            var secondContext = Manager.EstablishForCommand(command);
                                                                            secondContext.ShouldEqual(commandContext);
                                                                        };
    }
}
