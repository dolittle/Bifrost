using System;
using System.Windows.Input;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_executing_command_that_can_execute_with_arguments
    {
        static string argument = "Hello world";
        static Mock<IViewModel> view_model_mock;
        static ICommand command;

        Establish context = () =>
        {
            view_model_mock = new Mock<IViewModel>();
            command = new CommandForMethod(view_model_mock.Object, "MethodWithOneParameter");
        };

        Because of = () => command.Execute(argument);

        It should_call_the_method = () => view_model_mock.Verify(v=>v.MethodWithOneParameter(argument), Times.Once());
    }
}
