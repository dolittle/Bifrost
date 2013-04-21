using Bifrost.Validation;
using FluentValidation.Validators;

namespace Bifrost.Web.Mvc.Specs.Validation.for_ValidatorPropertyValidator
{
    public class MyValidator : PropertyValidatorWithDynamicState
    {
        public bool Something { get; set; }


        public MyValidator() : base("") { }

        public string TheString { get; private set; }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            TheString = DynamicState.TheString;
            return true;
        }
    }
}
