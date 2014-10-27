using System;
using Bifrost.Rules;
using Bifrost.Validation;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_MaxLength
{
    public class when_checking_value_that_is_longer
    {
        static string value = "12345";
        static MaxLength rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new MaxLength(null, 4);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_fail_with_length_is_too_long_as_reason = () => rule_context_mock.Verify(r => r.Fail(rule, value, Reasons.LengthIsTooLong), Times.Once());
    }
}
