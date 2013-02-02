using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter<,>))]
    public class when_converting_from_concept_as_long_to_json : given.concept_converters
    {
        static string tag = "ConceptAsStringId";
        static long value = 123456;
        static ConceptAsLong concept;
        static string json_representation;

        Establish context = () => concept = new ConceptAsLong { Value = value };

        Because of = () => json_representation = converter_of_string_concept.ConvertFrom(tag,concept,false);

        It should_serialize_the_guid = () => json_representation.Contains(value.ToString()).ShouldBeTrue();
    }
}
