using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_Value
{
    public class when_getting_hashcode_for_values_with_different_values : given.value_objects
    {
        static int first_hashcode;
        static int second_hashcode;

        Because of = () =>
            {
                first_hashcode = first_value.GetHashCode();
                second_hashcode = different_value.GetHashCode();
            };

        It should_have_different_hashcodes = () => first_hashcode.ShouldNotEqual(second_hashcode);
    }
}