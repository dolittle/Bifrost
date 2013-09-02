using Bifrost.Concepts;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.QuickStart.Concepts.Persons
{
    public class SocialSecurityNumber : ConceptAs<string>
    {
        public static implicit operator SocialSecurityNumber(string socialSecurityNumber)
        {
            return new SocialSecurityNumber { Value = socialSecurityNumber };
        }
    }

    public class SocialSecurityNumberInputValidator : InputValidator<SocialSecurityNumber>
    {
        public SocialSecurityNumberInputValidator()
        {
            RuleFor(ssn => ssn.Value)
                .Length(6);
        }
    }

    public class AnotherSocialSecurityNumberInputValidator : InputValidator<SocialSecurityNumber>
    {
        public AnotherSocialSecurityNumberInputValidator()
        {
            RuleFor(ssn => ssn.Value)
                .NotEmpty();
        }
    }

    public class SocialSecurityNumberBusinessValidator : BusinessValidator<SocialSecurityNumber>
    {
        public SocialSecurityNumberBusinessValidator()
        {
            RuleFor(ssn => ssn.Value)
                .Must(s => s == "123456");
        }
    }

    public class AnotherSocialSecurityNumberBusinessValidator : BusinessValidator<SocialSecurityNumber>
    {
        public AnotherSocialSecurityNumberBusinessValidator()
        {
            RuleFor(ssn => ssn.Value)
                .Equal("123456");
        }
    }
}