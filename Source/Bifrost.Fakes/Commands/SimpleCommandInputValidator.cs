using Bifrost.Commands;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Fakes.Commands
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