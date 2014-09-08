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
        static bool result;
        static Regex rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new Regex("[a-z]*");
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => result = rule.IsSatisfiedBy(rule_context_mock.Object, "something");

        It should_be_valid = () => result.ShouldBeTrue();
    }
}
