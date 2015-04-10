using System;
using System.Windows.Input;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_asking_if_can_execute_with_property
    {
        static Mock<IViewModel> view_model_mock;
        static ICommand command;

        Establish context = () =>
        {
            view_model_mock = new Mock<IViewModel>();
            command = new CommandForMethod(view_model_mock.Object, "MethodWithOneParameter", "CanExecutePropertyReturningBool");
        };

        Because of = () => command.CanExecute(null);

        It should_ask_the_can_execute_property = () => view_model_mock.VerifyGet(v => v.CanExecutePropertyReturningBool, Times.Once());
    }
}
