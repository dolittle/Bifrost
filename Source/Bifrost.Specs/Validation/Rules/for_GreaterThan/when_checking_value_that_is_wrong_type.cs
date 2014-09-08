using System;
using Bifrost.Rules;
using Bifrost.Validation;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_GreaterThan
{
    public class when_checking_value_that_is_wrong_type
    {
        static bool result;
        static GreaterThan<double>  rule;
        static Mock<IRuleContext> rule_context_mock;
        static Exception exception;

        Establish context = () => 
        {
            rule = new GreaterThan<double>(42.0);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => exception = Catch.Exception(() => result = rule.IsSatisfiedBy(rule_context_mock.Object, "string"));

        It should_throw_value_type_mismatch = () => exception.ShouldBeOfExactType<ValueTypeMismatch>();
    }
}
