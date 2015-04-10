using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_executing_command_that_can_execute_with_argument_and_value_converter
    {
        static string argument = "Hello world";
        static string converted_argument = "Something else";
        static Mock<IViewModel> view_model_mock;
        static ICommand command;
        static Mock<IValueConverter> value_converter_mock;

        Establish context = () =>
        {
            view_model_mock = new Mock<IViewModel>();
            value_converter_mock = new Mock<IValueConverter>();
            value_converter_mock.Setup(v => v.Convert(argument, typeof(object), Moq.It.IsAny<object>(), CultureInfo.CurrentUICulture)).Returns(converted_argument);
            command = new CommandForMethod(view_model_mock.Object, "MethodWithOneParameter", parameterConverter: value_converter_mock.Object);
        };

        Because of = () => command.Execute(argument);

        It should_call_the_method_with_converted_argument = () => view_model_mock.Verify(v=>v.MethodWithOneParameter(converted_argument), Times.Once());
    }
}
