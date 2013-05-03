using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_Value
{
    [Subject(typeof(Value<>))]
    public class when_equating_two_instances_with_different_values_using_the_equals_operator : given.value_objects
    {
        static bool is_equal;

        Because of = () => is_equal = first_value == different_value;

        It should_not_be_equal = () => is_equal.ShouldBeFalse();
    }
}