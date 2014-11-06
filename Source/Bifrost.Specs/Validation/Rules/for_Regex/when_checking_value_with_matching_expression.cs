using System;
using Bifrost.Rules;
using Bifrost.Validation;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_Regex
{
    public class when_checking_value_with_matching_expression
    {
        static string value = "something";
        static Regex rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new Regex(null, "[a-z]*");
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_not_fail = () => rule_context_mock.Verify(r => r.Fail(rule, value, Moq.It.IsAny<BrokenRuleReason>()), Times.Never());
    }
}
