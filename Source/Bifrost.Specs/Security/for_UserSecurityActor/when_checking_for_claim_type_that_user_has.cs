using System.Security.Claims;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_UserSecurityActor
{
    public class when_checking_for_claim_type_that_user_has
    {
        const string claim_type = "Something";
        static UserSecurityActor actor;
        static bool result;

        Establish context = () =>
        {
            var identity = new ClaimsIdentity();
            var principal = new ClaimsPrincipal(identity);

            identity.AddClaim(new Claim(claim_type, "42"));

            var principalResolver = new Mock<ICanResolvePrincipal>();
            principalResolver.Setup(p => p.Resolve()).Returns(principal);
            
            actor = new UserSecurityActor(principalResolver.Object);
        };

        Because of = () => result = actor.HasClaimType(claim_type);

        It should_return_true = () => result.ShouldBeTrue();
    }
}
