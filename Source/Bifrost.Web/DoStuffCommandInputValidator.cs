using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Web
{
	public class DoStuffCommandInputValidator : CommandInputValidator<DoStuffCommand>
	{
		public DoStuffCommandInputValidator ()
		{
			RuleFor (c=>c.StringParameter).NotEmpty();
		}
	}
}

