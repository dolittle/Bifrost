using Bifrost.Rules;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_GreaterThanOrEqual
{
    public class when_checking_value_that_is_less_than
    {
        static double value = 41.0;
        static GreaterThanOrEqual<double> rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new GreaterThanOrEqual<double>(42.0);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_fail_with_value_is_less_than_reason = () => rule_context_mock.Verify(r => r.Fail(rule, value, Reasons.ValueIsLessThan), Times.Once());
    }
}
