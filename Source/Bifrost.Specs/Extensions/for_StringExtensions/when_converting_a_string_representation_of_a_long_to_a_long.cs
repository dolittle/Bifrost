using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_StringExtensions
{
    [Subject(typeof(StringExtensions))]
    public class when_converting_a_string_representation_of_a_long_to_a_long
    {
        static string long_as_a_string;
        static long result;

        Establish context = () =>
            {
                long_as_a_string = "7654321";
            };

        Because of = () => result = (long)long_as_a_string.ParseTo(typeof(long));

        It should_create_a_long = () => result.ShouldBeOfType<long>();
        It should_have_the_correct_value = () => result.ToString().ShouldEqual(long_as_a_string);
    }
}