using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_decimal_concept_with_coming_in_as_decimal
    {
        static DecimalConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(DecimalConcept), 5d) as DecimalConcept;

        It should_hold_the_decimal = () => result.Value.ShouldEqual(5m);
    }
}