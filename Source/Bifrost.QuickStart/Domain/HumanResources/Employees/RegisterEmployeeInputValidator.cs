using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.QuickStart.Domain.HumanResources.Employees
{
    public class RegisterEmployeeInputValidator : CommandInputValidator<RegisterEmployee>
    {
        public RegisterEmployeeInputValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required");
        }
    }
}