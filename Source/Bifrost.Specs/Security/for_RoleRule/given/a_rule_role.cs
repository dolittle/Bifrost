using System.Linq;
using Bifrost.Security;
using Moq;

namespace Bifrost.Specs.Security.for_RoleRule.given
{
    public class a_rule_role
    {
        protected static string required_role;
        protected static Mock<IUserSecurityActor> user;
        protected static RoleRule rule;

        public a_rule_role()
        {
            required_role = "MY_ROLE";
            user = new Mock<IUserSecurityActor>();
            user.Setup(m => m.IsInRole(It.IsAny<string>())).Returns(false);
            rule = new RoleRule(user.Object,required_role);
        }

        protected static void SetUserRole(string role)
        {
            SetUserRoles(new[] {role});
        }

        protected static void SetUserRoles(string[] roles)
        {
            user.Setup(m => m.IsInRole(It.IsAny<string>()))
                .Returns((string r) => roles.Any(s => s == r));
        }
    }
}