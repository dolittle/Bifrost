using System;
using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter<,>))]
    public class when_converting_from_json_to_concept_as_guid : given.concept_converters
    {
        static Guid guid = Guid.NewGuid();
        static ConceptAsGuid concept;
        static object deserialized_object;

        Establish context = () =>
        {
            concept = new ConceptAsGuid { Value = guid };
        };

        Because of = () => deserialized_object = converter_of_guid_concept.ConvertTo(guid.ToString());

        It should_create_a_concept_of_guid = () => deserialized_object.ShouldBeOfType<ConceptAsGuid>();
        It should_populate_the_correct_value = () => ((ConceptAsGuid)deserialized_object).Value.ShouldEqual(guid);
    }
}
