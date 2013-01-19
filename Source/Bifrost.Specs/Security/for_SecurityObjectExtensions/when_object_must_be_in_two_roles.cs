using Bifrost.Security;
using Machine.Specifications;
using System.Collections.Generic;

namespace Bifrost.Specs.Security.for_SecurityObjectExtensions
{
    public class when_object_must_be_in_two_roles : given.a_fresh_security_object
    {
        const string first_role = "FirstRole";
        const string second_role = "SecondRole";

        static List<RoleRule> rules = new List<RoleRule>();

        Establish context = () => security_object_mock.Setup(s => s.AddRule(Moq.It.IsAny<RoleRule>())).Callback((ISecurityRule r) => rules.Add((RoleRule)r));

        Because of = () => security_object.MustBeInRoles(first_role, second_role);

        It should_add_first_role_as_rule = () => rules[0].Role.ShouldEqual(first_role);
        It should_add_second_role_as_rule = () => rules[1].Role.ShouldEqual(second_role);
    }
}
