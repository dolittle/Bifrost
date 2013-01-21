using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter))]
    public class when_converting_from_json_to_concept_as_string : given.a_concept_type_converter
    {
        static string tag = "ConceptAStringId";
        static string value = "What have the Romans ever done for us?";
        static given.ConceptAsString concept;
        static string json_representation;
        static object deserialized_object;

        Establish context = () =>
        {
            concept = new given.ConceptAsString { Value = value };
            json_representation = converter.ConvertFrom(tag, concept, false);
        };

        Because of = () => deserialized_object = converter.ConvertTo(json_representation);

        It should_create_a_concept_of_guid = () => deserialized_object.ShouldBeOfType<given.ConceptAsString>();
        It should_populate_the_correct_value = () => ((given.ConceptAsString)deserialized_object).Value.ShouldEqual(value);
    }
}
