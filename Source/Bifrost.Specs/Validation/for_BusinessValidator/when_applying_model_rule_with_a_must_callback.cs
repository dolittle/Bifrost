using Bifrost.Specs.Validation.for_Validator;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_BusinessValidator
{
    public class when_applying_model_rule_with_a_must_callback
    {
        static ValidatorWithModelRuleWithOneMustClause validator;
        static SimpleObject object_to_validate;

        Establish context = () =>
        {
            validator = new ValidatorWithModelRuleWithOneMustClause();
            object_to_validate = new SimpleObject();
        };

        Because of = () => validator.Validate(object_to_validate);

        It should_call_the_callback = () => validator.CallbackCalled.ShouldBeTrue();
    }
}
