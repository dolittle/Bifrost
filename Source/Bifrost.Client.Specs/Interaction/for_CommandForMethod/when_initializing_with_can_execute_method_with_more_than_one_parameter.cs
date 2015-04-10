using System;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_initializing_with_can_execute_method_with_more_than_one_parameter
    {
        static Mock<IViewModel> view_model_mock;
        static Exception exception;

        Establish context = () => view_model_mock = new Mock<IViewModel>();

        Because of = () => exception = Catch.Exception(() => new CommandForMethod(view_model_mock.Object, "MethodWithoutParameters", "CanExecuteMethodWithTwoParameters"));

        It should_throw_more_than_one_parameter = () => exception.ShouldBeOfExactType<MoreThanOneParameter>();
    }
}
