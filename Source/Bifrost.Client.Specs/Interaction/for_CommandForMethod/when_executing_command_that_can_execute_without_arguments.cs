using System;
using System.Windows.Input;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_executing_command_that_can_execute_without_arguments
    {
        static Mock<IViewModel> view_model_mock;
        static ICommand command;

        Establish context = () =>
        {
            view_model_mock = new Mock<IViewModel>();
            command = new CommandForMethod(view_model_mock.Object, "MethodWithoutParameters");
        };

        Because of = () => command.Execute(null);

        It should_call_the_method = () => view_model_mock.Verify(v=>v.MethodWithoutParameters(), Times.Once());
    }
}
