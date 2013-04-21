using Machine.Specifications;
using Bifrost.Commands;
using System;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_without_name_specified : given.an_empty_command_builder
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => builder.GetInstance());

        It should_throw_command_name_missing_exception = () => exception.ShouldBeOfType<CommandNameMissingException>();
    }
}
