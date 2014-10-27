using Bifrost.Rules;
using Bifrost.Validation.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.Rules.for_Email
{
    public class when_checking_value_that_is_missing_domain_suffix
    {
        static string value = "something@someplace";
        static Email rule;
        static Mock<IRuleContext> rule_context_mock;

        Establish context = () => 
        {
            rule = new Email(null);
            rule_context_mock = new Mock<IRuleContext>();
        };

        Because of = () => rule.Evaluate(rule_context_mock.Object, value);

        It should_fail_with_invalid_email_as_reason = () => rule_context_mock.Verify(r => r.Fail(rule, value, Email.InvalidEMailReason), Times.Once());
    }
}
