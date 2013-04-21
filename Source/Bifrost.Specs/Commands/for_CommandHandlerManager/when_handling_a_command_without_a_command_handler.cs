using System;
using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Commands.for_CommandHandlerManager
{
    public class when_handling_a_command_without_a_command_handler : given.a_command_handler_manager
    {
        static Exception thrown_exception;
        static ICommand handled_command;

        Because of = () =>
                         {
                             var commandMock = new Mock<ICommand>();
                             handled_command = commandMock.Object;
                             thrown_exception = Catch.Exception(() => manager.Handle(handled_command));
                         };

        It should_throw_unhandled_command_exception = () => thrown_exception.ShouldBeOfType<UnhandledCommandException>();
        It should_have_an_instance_of_the_command_in_the_exception = () => ((UnhandledCommandException) thrown_exception).Command.ShouldEqual(handled_command);
    }
}
