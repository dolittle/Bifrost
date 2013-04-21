using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_StringExtensions
{
    public class when_converting_a_string_with_camel_casing_to_pascal_casing
    {
        static string result;

        Because of = () => result = "camelCased".ToPascalCase();

        It should_turn_it_into_pascal_case = () => result.ShouldEqual("CamelCased");
    }
}
