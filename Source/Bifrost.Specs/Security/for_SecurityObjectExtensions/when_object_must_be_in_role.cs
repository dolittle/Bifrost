using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityObjectExtensions
{
    public class when_object_must_be_in_role : given.a_fresh_security_object
    {
        const string role = "SomeRole";

        static RoleRule rule;

        Establish context = () => security_object_mock.Setup(s => s.AddRule(Moq.It.IsAny<RoleRule>())).Callback((ISecurityRule r) => rule = (RoleRule)r);

        Because of = () => security_object.MustBeInRole(role);

        It should_add_a_role_rule = () => security_object_mock.Verify(s => s.AddRule(Moq.It.IsAny<RoleRule>()), Moq.Times.Once());
        It should_forward_the_correct_role_to_the_rule = () => rule.Role.ShouldEqual(role);
    }
}
