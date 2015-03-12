using System;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_initializing_with_method_that_does_not_exist
    {
        static Mock<IViewModel> view_model_mock;
        static Exception exception;

        Establish context = () => view_model_mock = new Mock<IViewModel>();

        Because of = () => exception = Catch.Exception(() => new CommandForMethod(view_model_mock.Object, "Something"));

        It should_throw_missing_method_for_command = () => exception.ShouldBeOfExactType<MissingMethodForCommand>();
    }
}
