using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_ClaimTypeRule.given
{
    public class a_claim_type_rule
    {
        protected const string required_claim = "MY CLAIM";
        protected static Mock<IUserSecurityActor> user;
        protected static ClaimTypeRule rule;

        Establish context = () =>
        {
            user = new Mock<IUserSecurityActor>();
            rule = new ClaimTypeRule(user.Object, required_claim);
        };
    }
}