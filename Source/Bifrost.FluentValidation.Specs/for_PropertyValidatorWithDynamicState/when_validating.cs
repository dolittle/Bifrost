using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_PropertyValidatorWithDynamicState
{
    public class when_validating
    {
        const string expected = "42";
        static Model model;
        static MyValidator validator;
        static PropertyValidatorContext validator_context;

        Establish context = () =>
            {
                validator = new MyValidator();
                model = new Model { TheString = expected };
                validator_context = new PropertyValidatorContext(
                    new ValidationContext(model), 
                    PropertyRule.Create((Model m) => m.TheString),
                    "TheString");
                validator.AddExpression<Model>(v => v.TheString);
            };

        Because of = () => validator.Validate(validator_context);

        It should_have_expected_value_as_dynamic_state = () => validator.TheString.ShouldEqual(expected);
    }
}
