using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_ClaimTypeRule
{
    [Subject(typeof (ClaimTypeRule))]
    public class when_authorizing_with_user_who_does_not_have_role : given.a_claim_type_rule
    {
        static bool result;

        Because of = () => result = rule.IsAuthorized(new object());

        It should_not_be_authorized = () => result.ShouldBeFalse();
    }
}