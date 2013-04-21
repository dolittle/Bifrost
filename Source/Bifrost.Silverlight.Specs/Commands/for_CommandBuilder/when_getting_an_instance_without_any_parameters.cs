using System;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_without_any_parameters : given.an_empty_command_builder
    {
        static Exception exception;

        Establish context = () => builder.Name = "Test";

        Because of = () => exception = Catch.Exception(() => builder.GetInstance());

        It should_not_throw_any_exceptions = () => exception.ShouldBeNull();
    }
}
