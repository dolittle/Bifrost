using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptAs
{
    [Subject(typeof(ConceptAs<>))]
    public class when_checking_is_empty_on_a_populated_string_concept : given.concepts
    {
        static bool is_empty;

        Establish context = () => is_empty = first_string.IsEmpty();

        It should_not_be_empty = () => is_empty.ShouldBeFalse();
    }
}