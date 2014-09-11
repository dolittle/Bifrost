using Bifrost.Rules;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_LessThan
{
    public class when_checking_value_that_is_greater_than
    {
        static double value = 43.0;
        static LessThan<double> rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new LessThan<double>(42.0);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_fail_with_value_is_greater_than_reason = () => rule_context_mock.Verify(r => r.Fail(rule, value, Reasons.ValueIsGreaterThan), Times.Once());
    }
}
