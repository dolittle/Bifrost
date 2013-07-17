using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptAs
{
    [Subject(typeof(ConceptAs<>))]
    public class when_checking_is_empty_on_a_null_string_concept : given.concepts
    {
        static bool is_empty;

        Establish context = () => is_empty = string_is_null.IsEmpty();

        It should_be_empty = () => is_empty.ShouldBeTrue();
    }
}