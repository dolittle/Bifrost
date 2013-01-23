using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter))]
    public class when_converting_from_concept_as_long_to_json : given.a_concept_type_converter
    {
        static string tag = "ConceptAsStringId";
        static long value = 123456;
        static given.ConceptAsLong concept;
        static string json_representation;

        Establish context = () => concept = new given.ConceptAsLong { Value = value };

        Because of = () => json_representation = converter.ConvertFrom(tag,concept,false);

        It should_serialize_the_guid = () => json_representation.Contains(value.ToString());
        It should_serialize_the_concrete_type = () =>
        {
            json_representation.Contains(concept.GetType().FullName);
            json_representation.Contains(concept.GetType().AssemblyQualifiedName);
        };
    }
}
