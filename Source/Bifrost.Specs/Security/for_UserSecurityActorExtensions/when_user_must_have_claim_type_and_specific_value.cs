using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_UserSecurityActorExtensions
{
    public class when_user_must_have_claim_type_and_specific_value
    {
        static UserSecurityActor    actor;

        Establish context = () => actor = new UserSecurityActor(Mock.Of<ICanResolvePrincipal>());

        Because of = () => actor.MustHaveClaimTypeWithValue("Something","42");

        It should_add_a_claim_type_rule_to_the_actor = () => actor.Rules.First().ShouldBeOfExactType<ClaimTypeAndValueRule>();
        It should_pass_required_claim_type_to_the_rule = () => ((ClaimTypeAndValueRule)actor.Rules.First()).ClaimType.ShouldEqual("Something");
        It should_pass_required_value_to_the_rule = () => ((ClaimTypeAndValueRule)actor.Rules.First()).Value.ShouldEqual("42");
    }
}
