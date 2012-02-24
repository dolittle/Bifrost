using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Web
{
	public class DoStuffCommandInputValidator : CommandInputValidator<DoStuffCommand>
	{
		public DoStuffCommandInputValidator ()
		{
            RuleFor(c => c.StringParameter)
                .NotEmpty()
                    .WithMessage("You really should not have this empty")
                .EmailAddress()
                    .WithMessage("Not a valid email address");
                

            RuleFor(c => c.IntParameter)
                .GreaterThan(5)
                    .WithMessage("Should be greater than 5")
                .LessThan(10)
                    .WithMessage("Should less and 10");
		}
	}
}

