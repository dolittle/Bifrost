using Bifrost.Concepts;
using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class ConceptAsStringValidator : BusinessValidator<ConceptAsString>
    {
        public ConceptAsStringValidator()
        {
            RuleFor(c => c.Value).NotEmpty();
        }
    }

    public class ConceptAsLongValidator : BusinessValidator<ConceptAsLong>
    {
        public ConceptAsLongValidator()
        {
            ModelRule()
                .NotNull()
                .SetValidator(new LongValidator());
        }
    }

    public class LongValidator: BusinessValidator<ConceptAs<long>>
    {
        public LongValidator()
        {
            RuleFor(c => c.Value)
                .NotEmpty()
                .GreaterThan(0);
        }
    }

    public class ObjectValidator : BusinessValidator<object>
    {
        public ObjectValidator()
        {
            ModelRule()
                .NotNull();
        }
    }
}
