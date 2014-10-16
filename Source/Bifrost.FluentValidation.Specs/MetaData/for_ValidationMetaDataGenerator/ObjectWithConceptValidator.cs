using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class ObjectWithConceptValidator : BusinessValidator<ObjectWithConcept>
    {
        public ObjectWithConceptValidator()
        {
            RuleForConcept(o => o.StringConcept)
                .NotNull()
                .SetValidator(new ConceptAsStringValidator());
            RuleForConcept(o => o.LongConcept)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(o => o.NonConceptObject)
                .NotNull()
                .SetValidator(new ObjectValidator());
        }
    }
}
