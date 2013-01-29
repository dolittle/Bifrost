using Bifrost.Testing.Fakes.Concepts;

namespace Bifrost.JSON.Specs.Serialization.for_Serializer
{
    public class ClassWithConcepts
    {
        public ConceptAsGuid GuidConcept { get; set; }
        public ConceptAsString StringConcept { get; set; }
        public ConceptAsLong LongConcept { get; set; }
    }
}