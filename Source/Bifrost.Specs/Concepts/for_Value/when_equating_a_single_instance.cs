using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_Value
{
    [Subject(typeof(Value<>))]
    public class when_equating_a_single_instance : given.value_objects
    {
        static bool is_equal;

        Because of = () => is_equal = first_value.Equals(first_value);

        It should_be_equal = () => is_equal.ShouldBeTrue();
    }
}