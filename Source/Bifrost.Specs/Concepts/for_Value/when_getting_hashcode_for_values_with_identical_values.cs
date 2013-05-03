using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_Value
{
    public class when_getting_hashcode_for_values_with_identical_values : given.value_objects
    {
        static int first_hashcode;
        static int second_hashcode;

        Because of = () =>
            {
                first_hashcode = first_value.GetHashCode();
                second_hashcode = identical_values_to_first_value.GetHashCode();
            };

        It should_have_identical_hashcodes = () => first_hashcode.ShouldEqual(second_hashcode);
    }
}