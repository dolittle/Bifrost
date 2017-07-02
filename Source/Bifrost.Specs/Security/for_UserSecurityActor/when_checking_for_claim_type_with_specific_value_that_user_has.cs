using System.Security.Claims;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_UserSecurityActor
{
    public class when_checking_for_claim_type_with_specific_value_that_user_has : given.a_user_security_actor
    {
        const string claim_type = "Something";
        static bool result;

        Establish context = () => identity.AddClaim(new Claim(claim_type, "42"));

        Because of = () => result = actor.HasClaimTypeWithValue(claim_type, "42");

        It should_return_true = () => result.ShouldBeTrue();
    }
}