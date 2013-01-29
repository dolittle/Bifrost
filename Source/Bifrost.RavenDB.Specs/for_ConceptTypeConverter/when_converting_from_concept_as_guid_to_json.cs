using System;
using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter<,>))]
    public class when_converting_from_concept_as_guid_to_json : given.concept_converters
    {
        static string tag = "ConceptAsGuidId";
        static Guid guid = Guid.NewGuid();
        static ConceptAsGuid concept;
        static string json_representation;

        Establish context = () => concept = new ConceptAsGuid{ Value = guid };

        Because of = () => json_representation = converter_of_guid_concept.ConvertFrom(tag,concept,false);

        It should_serialize_the_guid = () => json_representation.Contains(guid.ToString()).ShouldBeTrue();
    }
}
