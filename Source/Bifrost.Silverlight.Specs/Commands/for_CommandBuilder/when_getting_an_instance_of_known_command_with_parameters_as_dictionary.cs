using System.Collections.Generic;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_of_known_command_with_parameters_as_dictionary : given.an_empty_command_builder_for_known_command
    {
        static KnownCommand result;

        Establish context = () =>
        {
            builder.Name = "Known";
            builder.Parameters = new Dictionary<string, object>
            {
                { "Integer", 42 },
                { "Something", "It is something" }
            };
        };

        Because of = () => result = builder.GetInstance();

        It should_set_the_integer_property_on_the_command = () => result.Integer.ShouldEqual(42);
        It should_set_the_string_property_on_the_command = () => result.Something.ShouldEqual("It is something");
        It should_have_integer_value_in_parameters = () => ShouldExtensionMethods.ShouldEqual(result.Parameters.Integer, 42);
        It should_have_string_value_in_parameters = () => ShouldExtensionMethods.ShouldEqual(result.Parameters.Something, "It is something");
    }
}
