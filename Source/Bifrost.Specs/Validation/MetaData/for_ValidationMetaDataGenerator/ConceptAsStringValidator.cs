using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class ConceptAsStringValidator : Validator<ConceptAsString>
    {
        public ConceptAsStringValidator()
        {
            RuleFor(c => c.Value).NotEmpty();
        }
    }
}
