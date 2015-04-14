using Bifrost.FluentValidation.Commands;
using Bifrost.Testing.Fakes.Commands;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs.Commands
{
    public class SimpleCommandBusinessValidator : CommandBusinessValidator<SimpleCommand>
    {
        public SimpleCommandBusinessValidator()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();
            RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
        }
    }
}