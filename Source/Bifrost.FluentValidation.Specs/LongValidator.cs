using Bifrost.Concepts;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs
{
    public class LongValidator : BusinessValidator<ConceptAs<long>>
    {
        public LongValidator()
        {
            RuleFor(c => c.Value)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
