using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptExtensions
{
    [Subject(typeof(ConceptExtensions))]
    public class when_checking_is_concept_on_an_inherited_concept_instance : given.concepts
    {
        static bool is_a_concept;

        Because of = () => is_a_concept = value_as_a_long_inherited.IsConcept();

        It should_be_a_concept = () => is_a_concept.ShouldBeTrue();
    }
}