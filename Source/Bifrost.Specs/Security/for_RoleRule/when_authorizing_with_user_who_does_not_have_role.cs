using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_RoleRule
{
    [Subject(typeof (RoleRule))]
    public class when_authorizing_with_user_who_does_not_have_role : given.a_rule_role
    {
        static bool is_authorized;

        Because of = () => is_authorized = rule.IsAuthorized(new object());

        It should_not_be_authorized = () => is_authorized.ShouldBeFalse();
    }
}