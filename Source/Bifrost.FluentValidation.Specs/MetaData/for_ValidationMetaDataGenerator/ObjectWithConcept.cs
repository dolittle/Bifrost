using Bifrost.Testing.Fakes.Concepts;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class ObjectWithConcept
    {
        public ConceptAsString StringConcept { get; set; }
        public ConceptAsLong LongConcept { get; set; }
        public object NonConceptObject { get; set; }
    }
}
