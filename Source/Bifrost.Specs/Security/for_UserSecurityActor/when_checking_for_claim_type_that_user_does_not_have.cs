using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_UserSecurityActor
{
    public class when_checking_for_claim_type_that_user_does_not_have : given.a_user_security_actor
    {
        static bool result;

        Because of = () => result = actor.HasClaimType("Something");

        It should_return_false = () => result.ShouldBeFalse();
    }
}
