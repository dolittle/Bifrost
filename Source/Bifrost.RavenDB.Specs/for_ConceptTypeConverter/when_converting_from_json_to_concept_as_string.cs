using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter<,>))]
    public class when_converting_from_json_to_concept_as_string : given.concept_converters
    {
        static string tag = "ConceptAsStringId/";
        static string value = "What have the Romans ever done for us?";
        static ConceptAsString concept;
        static string json_representation;
        static object deserialized_object;

        Establish context = () =>
        {
            concept = new ConceptAsString { Value = value };
        };

        Because of = () => deserialized_object = converter_of_string_concept.ConvertTo(value);

        It should_create_a_concept_of_guid = () => deserialized_object.ShouldBeOfExactType<ConceptAsString>();
        It should_populate_the_correct_value = () => ((ConceptAsString)deserialized_object).Value.ShouldEqual(value);
    }
}
