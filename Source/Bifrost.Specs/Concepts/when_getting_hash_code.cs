using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts
{
    [Subject(typeof(ConceptAs<>))]
    public class when_getting_hash_code : given.concepts
    {
        static bool hash_codes_of_different_values_are_different;
        static bool hash_codes_of_same_values_are_the_same;
        static bool hash_code_of_same_underlying_values_of_different_types_are_different;

        Because of = () =>
            {
                var hash_code_of_first = first_string.GetHashCode();
                var hash_code_of_second = second_string.GetHashCode();
                var hash_code_of_same_value_of_second = same_value_as_second_string.GetHashCode();
                var hash_code_of_value_as_a_long = value_as_a_long.GetHashCode();
                var hash_code_of_value_as_an_int = value_as_an_int.GetHashCode();

                hash_codes_of_different_values_are_different = hash_code_of_first != hash_code_of_second;
                hash_codes_of_same_values_are_the_same = hash_code_of_second == hash_code_of_same_value_of_second;
                hash_code_of_same_underlying_values_of_different_types_are_different = hash_code_of_value_as_an_int !=
                                                                                       hash_code_of_value_as_a_long;
            };

        It should_have_different_hash_codes_for_differing_values = () => hash_codes_of_different_values_are_different.ShouldBeTrue();
        It should_have_the_same_hash_codes_for_same_values = () => hash_codes_of_same_values_are_the_same.ShouldBeTrue();
        It should_have_different_hash_codes_for_same_values_of_differing_types = () => hash_code_of_same_underlying_values_of_different_types_are_different.ShouldBeTrue();

    }
}