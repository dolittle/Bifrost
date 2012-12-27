using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_of_command_with_constructor_and_parameters_are_missing : given.an_empty_command_builder_for_known_command_with_constructor_parameter
    {
        static Exception exception;

        Establish context = () => builder.WithParameters(p => { });

        Because of = () => exception = Catch.Exception(() => builder.GetInstance());

        It should_throw_command_constructor_parameter_missing_exception = () => exception.ShouldBeOfType<CommandConstructorParameterMissing>();
    }
}
