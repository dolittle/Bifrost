using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_Value
{
    public class when_getting_hashcode_for_values_with_transposed_values : given.value_objects
    {
        static int first_hashcode;
        static int second_hashcode;
        static string serialized;

        Because of = () =>
            {
                first_hashcode = first_value.GetHashCode();
                second_hashcode = transposed_values.GetHashCode();
            };

        It should_have_different_hashcodes = () => first_hashcode.ShouldNotEqual(second_hashcode);
    }
}