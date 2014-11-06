using FluentValidation;

namespace Bifrost.FluentValidation.Specs
{
    public class ObjectValidator : BusinessValidator<object>
    {
        public ObjectValidator()
        {
            ModelRule()
                .NotNull();
        }
    }
}
