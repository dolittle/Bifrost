using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityObject
{
    public class when_asking_if_has_access_with_one_rule_that_returns_false
    {
        const string securable = "something";
        static SecurityObject   security_object;
        static bool result;
        static Mock<ISecurityRule> security_rule_mock;

        Establish context = () =>
        {
            security_object = new SecurityObject();
            security_rule_mock = new Mock<ISecurityRule>();
            security_rule_mock.Setup(s => s.HasAccess(securable)).Returns(false);
            security_object.AddRule(security_rule_mock.Object);
        };

        Because of = () => result = security_object.HasAccess(securable);

        It should_return_false = () => result.ShouldBeFalse();
        It should_ask_the_rule = () => security_rule_mock.Verify(s => s.HasAccess(securable),Moq.Times.Once());
    }
}
