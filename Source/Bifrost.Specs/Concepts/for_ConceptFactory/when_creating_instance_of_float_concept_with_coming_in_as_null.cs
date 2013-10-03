using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    public class when_creating_instance_of_float_concept_with_coming_in_as_null
    {
        static FloatConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(FloatConcept), null) as FloatConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(0f);
    }
}
