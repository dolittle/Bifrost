using System.Security.Claims;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_UserSecurityActor
{
    public class when_checking_for_claim_type_that_user_does_not_have
    {
        static UserSecurityActor actor;
        static bool result;

        Establish context = () =>
        {
            var identity = new ClaimsIdentity();
            var principal = new ClaimsPrincipal(identity);

            var principalResolver = new Mock<ICanResolvePrincipal>();
            principalResolver.Setup(p => p.Resolve()).Returns(principal);

            actor = new UserSecurityActor(principalResolver.Object);
        };

        Because of = () => result = actor.HasClaimType("Something");

        It should_return_false = () => result.ShouldBeFalse();
    }
}
