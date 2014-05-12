using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.QuickStart.Domain.HumanResources.Employees
{
    public class RegisterEmployeeInputValidator : CommandInputValidator<RegisterEmployee>
    {
        public RegisterEmployeeInputValidator()
        {
            RuleFor(r => r.FirstName).Length(1,10).WithMessage("First name is required.  It can have a max length of 10 characters.");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required");
        }
    }
}