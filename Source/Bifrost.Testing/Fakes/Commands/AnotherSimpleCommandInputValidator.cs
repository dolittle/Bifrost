using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Testing.Fakes.Commands
{
    public class AnotherSimpleCommandInputValidator : CommandInputValidator<AnotherSimpleCommand>
    {
        public AnotherSimpleCommandInputValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}