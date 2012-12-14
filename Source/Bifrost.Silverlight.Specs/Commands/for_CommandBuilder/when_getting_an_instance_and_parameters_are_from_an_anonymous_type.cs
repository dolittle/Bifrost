using System;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_and_parameters_are_from_an_anonymous_type : given.an_empty_command_builder
    {
        static Exception exception;

        Establish context = () =>
        {
            builder.Name = "Test";
            builder.Parameters = new
            {
                Something = "Hello"
            };
        };

        Because of = () => exception = Catch.Exception(() => builder.GetInstance());

        It should_throw_unsupported_parameters_construct = () => exception.ShouldBeOfType<UnsupportedParametersConstruct>();
    }
}
