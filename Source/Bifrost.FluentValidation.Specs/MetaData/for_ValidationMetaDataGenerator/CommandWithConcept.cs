using Bifrost.Commands;
using Bifrost.Testing.Fakes.Concepts;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandWithConcept : Command
    {
        public ConceptAsString StringConcept { get; set; }
        public ConceptAsLong LongConcept { get; set; }
        public object NonConceptObject { get; set; }
    }
}
