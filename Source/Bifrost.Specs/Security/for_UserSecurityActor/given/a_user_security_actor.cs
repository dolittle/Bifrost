using System.Security.Claims;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_UserSecurityActor.given
{
    public class a_user_security_actor
    {
        protected static UserSecurityActor actor;
        protected static ClaimsIdentity identity;
        protected static ClaimsPrincipal principal;

        Establish context = () =>
        {
            identity = new ClaimsIdentity();
            principal = new ClaimsPrincipal(identity);

            var principalResolver = new Mock<ICanResolvePrincipal>();
            principalResolver.Setup(p => p.Resolve()).Returns(principal);

            actor = new UserSecurityActor(principalResolver.Object);
        };
    }
}