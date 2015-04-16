using Bifrost.Extensions;
using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_StringExtensions
{
    [Subject(typeof(StringExtensions))]
    public class when_converting_a_string_representation_of_a_long_to_a_long_concept
    {
        static string long_as_a_string;
        static ConceptAsLong result;

        Establish context = () =>
            {
                long_as_a_string = "7654321";
            };

        Because of = () => result = (ConceptAsLong)long_as_a_string.ParseTo(typeof(ConceptAsLong));

        It should_create_a_long_concept = () => result.ShouldBeOfExactType<ConceptAsLong>();
        It should_have_the_correct_value = () => result.ToString().ShouldEqual(long_as_a_string);
    }
}