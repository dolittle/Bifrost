using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class ObjectWithConceptValidator : Validator<ObjectWithConcept>
    {
        public ObjectWithConceptValidator()
        {
            RuleFor(o => o.StringConcept).SetValidator(new ConceptAsStringValidator());
            RuleFor(o => o.LongConcept)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
        }
    }
}
