using Bifrost.Testing.Fakes.Concepts;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs.for_ValidationMetaDataGenerator
{
    public class ConceptAsStringValidator : BusinessValidator<ConceptAsString>
    {
        public ConceptAsStringValidator()
        {
            RuleFor(c => c.Value).NotEmpty();
        }
    }
}
