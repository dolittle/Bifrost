using System;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_initializing_with_can_execute_property_returning_non_bool
    {
        static Mock<IViewModel> view_model_mock;
        static Exception exception;

        Establish context = () => view_model_mock = new Mock<IViewModel>();

        Because of = () => exception = Catch.Exception(() => new CommandForMethod(view_model_mock.Object, "MethodWithoutParameters", "CanExecutePropertyReturningNonBool"));

        It should_throw_return_value_should_be_boolean = () => exception.ShouldBeOfExactType<ReturnValueShouldBeBoolean>();
    }
}
