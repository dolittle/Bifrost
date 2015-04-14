using Bifrost.FluentValidation.Commands;
using Bifrost.Testing.Fakes.Commands;
using FluentValidation;

namespace Bifrost.FluentValidation.Specs.Commands
{
    public class SimpleCommandBusinessValidatorWithRuleset : CommandBusinessValidator<SimpleCommand>
    {
        public const string SERVER_ONLY_RULESET = "ServerOnly";

        public SimpleCommandBusinessValidatorWithRuleset()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();

            RuleSet(SERVER_ONLY_RULESET, () =>
            {
                RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
            });

        }
    }
}