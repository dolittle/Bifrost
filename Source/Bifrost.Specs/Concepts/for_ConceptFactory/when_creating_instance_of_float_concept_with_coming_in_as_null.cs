using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_float_concept_with_coming_in_as_null
    {
        static FloatConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(FloatConcept), null) as FloatConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(0f);
    }

    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_float_concept_with_coming_in_as_float
    {
        static FloatConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(FloatConcept), 5f) as FloatConcept;

        It should_hold_zero = () => result.Value.ShouldEqual(5f);
    }
}
