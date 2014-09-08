using System;
using Bifrost.Rules;
using Bifrost.Validation;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_Regex
{
    public class when_checking_value_that_is_wrong_type
    {
        static bool result;
        static Regex rule;
        static Mock<IRuleContext> rule_context_mock;
        static Exception exception;

        Establish context = () => 
        {
            rule = new Regex(string.Empty);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => exception = Catch.Exception(() => result = rule.IsSatisfiedBy(rule_context_mock.Object, 42));

        It should_throw_value_type_mismatch = () => exception.ShouldBeOfExactType<ValueTypeMismatch>();
    }
}
