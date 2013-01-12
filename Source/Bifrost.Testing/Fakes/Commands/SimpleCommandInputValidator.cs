using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Testing.Fakes.Commands
{
    public class SimpleCommandInputValidator : CommandInputValidator<SimpleCommand>
    {
        public SimpleCommandInputValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}