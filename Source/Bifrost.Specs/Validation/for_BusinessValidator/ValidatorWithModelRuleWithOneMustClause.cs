using Bifrost.Specs.Validation.for_BusinessValidator;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Specs.Validation.for_Validator
{
    public class ValidatorWithModelRuleWithOneMustClause : BusinessValidator<SimpleObject>
    {
        public bool CallbackCalled = false;
        public ValidatorWithModelRuleWithOneMustClause()
        {
            ModelRule().Must(o => CallbackCalled = true);
        }
    }
}
