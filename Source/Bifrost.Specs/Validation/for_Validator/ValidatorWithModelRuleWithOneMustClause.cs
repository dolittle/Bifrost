using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.for_Validator
{
    public class ValidatorWithModelRuleWithOneMustClause : Validator<SimpleObject>
    {
        public bool CallbackCalled = false;
        public ValidatorWithModelRuleWithOneMustClause()
        {
            ModelRule().Must(o => CallbackCalled = true);
        }
    }
}
