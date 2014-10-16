using Bifrost.Validation;
using FluentValidation.Validators;

namespace Bifrost.Specs.Validation.for_PropertyValidatorWithDynamicState
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
