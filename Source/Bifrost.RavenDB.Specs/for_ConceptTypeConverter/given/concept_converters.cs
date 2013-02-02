using System;
using Bifrost.Testing.Fakes.Concepts;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter.given
{
    public class concept_converters
    {
        protected static ConceptTypeConverter<ConceptAsString, string> converter_of_string_concept;
        protected static ConceptTypeConverter<ConceptAsGuid, Guid> converter_of_guid_concept;
        protected static ConceptTypeConverter<ConceptAsLong, long> converter_of_long_concept;

        public concept_converters()
        {
            converter_of_string_concept = new ConceptTypeConverter<ConceptAsString,string>();
            converter_of_guid_concept = new ConceptTypeConverter<ConceptAsGuid, Guid>();
            converter_of_long_concept = new ConceptTypeConverter<ConceptAsLong, long>();
        }
    }
}
