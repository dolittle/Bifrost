using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_guid_concept_with_value_as_null
    {
        static GuidConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(GuidConcept), null) as GuidConcept;

        It should_be_empty = () => result.IsEmpty().ShouldBeTrue();
    }
}