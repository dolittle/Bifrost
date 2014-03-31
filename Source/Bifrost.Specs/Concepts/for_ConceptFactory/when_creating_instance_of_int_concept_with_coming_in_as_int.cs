using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_int_concept_with_coming_in_as_int
    {
        static IntConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(IntConcept), 5) as IntConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(5);
    }
}