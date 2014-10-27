using Bifrost.Rules;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_LessThanOrEqual
{
    public class when_checking_value_that_is_less_than
    {
        static double value = 41.0;
        static LessThanOrEqual<double> rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new LessThanOrEqual<double>(null, 42.0);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_not_fail = () => rule_context_mock.Verify(r => r.Fail(rule, value, Moq.It.IsAny<BrokenRuleReason>()), Times.Never());
    }
}
