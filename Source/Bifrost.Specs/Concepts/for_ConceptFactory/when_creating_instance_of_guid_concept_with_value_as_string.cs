using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_guid_concept_with_value_as_string
    {
        const   string  guid_value_as_string = "4AB92720-3138-4A7B-A7E9-2A49F6624736";

        static GuidConcept result;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(GuidConcept), guid_value_as_string) as GuidConcept;

        It should_hold_the_correct_guid_value = () => result.Value.ToString().ToUpper().ShouldEqual(guid_value_as_string);
    }
}
