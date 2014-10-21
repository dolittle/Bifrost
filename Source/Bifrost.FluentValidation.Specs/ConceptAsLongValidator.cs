using Bifrost.Testing.Fakes.Concepts;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs
{
    public class ConceptAsLongValidator : BusinessValidator<ConceptAsLong>
    {
        public ConceptAsLongValidator()
        {
            ModelRule()
                .NotNull()
                .SetValidator(new LongValidator());
        }
    }
}
