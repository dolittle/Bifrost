using System;
using System.Windows.Input;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_asking_if_can_execute_with_method_with_one_parameter
    {
        static string argument = "Hello world";
        static Mock<IViewModel> view_model_mock;
        static ICommand command;

        Establish context = () =>
        {
            view_model_mock = new Mock<IViewModel>();
            command = new CommandForMethod(view_model_mock.Object, "MethodWithOneParameter", "CanExecuteMethodWithOneParameter");
        };

        Because of = () => command.CanExecute(argument);

        It should_ask_the_can_execute_method = () => view_model_mock.Verify(v => v.CanExecuteMethodWithOneParameter(argument), Times.Once());
    }
}
