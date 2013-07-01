using Bifrost.Concepts;
using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class ConceptAsStringValidator : Validator<ConceptAsString>
    {
        public ConceptAsStringValidator()
        {
            //RuleFor(c => c.Value).NotEmpty();
            ModelRule().NotEmpty();
        }
    }

    public class ConceptAsLongValidator : Validator<ConceptAsLong>
    {
        public ConceptAsLongValidator()
        {
            ModelRule()
                .NotNull()
                .SetValidator(new LongValidator());
        }
    }

    public class LongValidator: Validator<ConceptAs<long>>
    {
        public LongValidator()
        {
            RuleFor(c => c.Value)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
