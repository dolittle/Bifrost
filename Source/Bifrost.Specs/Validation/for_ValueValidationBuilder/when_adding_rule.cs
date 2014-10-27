using System.Reflection;
using Bifrost.Rules;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_ValueValidationBuilder
{
    public class when_adding_rule
    {
        static ValueValidationBuilder<object>   builder;
        static Mock<IValueRule>  rule_mock;

        Establish context = () => {
            builder = new ValueValidationBuilder<object>(null);
            rule_mock = new Mock<IValueRule>();
        };

        Because of = () => builder.AddRule(rule_mock.Object);

        It should_hold_the_rule = () => builder.Rules.ShouldContainOnly(rule_mock.Object);
    }
}
