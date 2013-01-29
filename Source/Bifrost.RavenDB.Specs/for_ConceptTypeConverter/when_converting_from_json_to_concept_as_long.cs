using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter<,>))]
    public class when_converting_from_json_to_concept_as_long : given.concept_converters
    {
        static string tag = "ConceptAsStringId";
        static long value = 7654321;
        static ConceptAsLong concept;
        static string json_representation;
        static object deserialized_object;

        Establish context = () =>
        {
            concept = new ConceptAsLong { Value = value };
            json_representation = converter_of_long_concept.ConvertFrom(tag, concept, false);
        };

        Because of = () => deserialized_object = converter_of_long_concept.ConvertTo(json_representation);

        It should_create_a_concept_of_long = () => deserialized_object.ShouldBeOfType<ConceptAsLong>();
        It should_populate_the_correct_value = () => ((ConceptAsLong)deserialized_object).Value.ShouldEqual(value);
    }
}
