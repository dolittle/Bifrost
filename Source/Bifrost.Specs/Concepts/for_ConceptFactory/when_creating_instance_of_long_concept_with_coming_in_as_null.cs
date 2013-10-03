using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_long_concept_with_coming_in_as_null
    {
        static LongConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(LongConcept), null) as LongConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(0);
    }
}
