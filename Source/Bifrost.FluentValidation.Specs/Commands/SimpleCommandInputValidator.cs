using Bifrost.FluentValidation.Commands;
using Bifrost.Testing.Fakes.Commands;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs.Commands
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