using System;
using Bifrost.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Rules.for_RuleContext
{
    public class when_failing_with_two_callbacks_registered 
    {
        static RuleContext rule_context;
        static Mock<IRule> rule_mock;
        static object instance;
        static BrokenRuleReason broken_rule_reason;

        static Mock<RuleFailed> first_failed_callback;
        static Mock<RuleFailed> second_failed_callback;

        Establish context = () =>
        {
            rule_mock = new Mock<IRule>();
            instance = new object();
            broken_rule_reason = BrokenRuleReason.Create(Guid.NewGuid().ToString());
            rule_context = new RuleContext();

            first_failed_callback = new Mock<RuleFailed>();
            rule_context.OnFailed(first_failed_callback.Object);

            second_failed_callback = new Mock<RuleFailed>();
            rule_context.OnFailed(second_failed_callback.Object);
        };

        Because of = () => rule_context.Fail(rule_mock.Object, instance, broken_rule_reason);

        It should_call_first_callback = () => first_failed_callback.Verify(c => c(rule_mock.Object, instance, broken_rule_reason), Moq.Times.Once());
        It should_call_second_callback = () => second_failed_callback.Verify(c => c(rule_mock.Object, instance, broken_rule_reason), Moq.Times.Once());
    }
}
