using System;
using Bifrost.Rules;
using Bifrost.Validation;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_Regex
{
    public class when_checking_value_with_non_matching_expression
    {
        static bool result;
        static Regex rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new Regex("\\w");
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => result = rule.IsSatisfiedBy(rule_context_mock.Object, "");

        It should_not_be_valid = () => result.ShouldBeFalse();
    }
}
