using System.Collections.Generic;
using Bifrost.Commands;
using Machine.Specifications;
using System.Dynamic;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_with_name_and_default_parameters_as_dynamic : given.an_empty_command_builder
    {
        static ICommand    result;

        Establish context = () =>
        {
            dynamic expando = new ExpandoObject();
            expando.Integer = 42;
            expando.String = "Hello world";

            builder.Name = "Test";
            builder.Parameters = expando;
        };

        Because of = () => result = builder.GetInstance();

        It should_have_integer_value_in_parameters = () => ShouldExtensionMethods.ShouldEqual(result.Parameters.Integer, 42);
        It should_have_string_value_in_parameters = () => ShouldExtensionMethods.ShouldEqual(result.Parameters.String, "Hello world");
    }
}
