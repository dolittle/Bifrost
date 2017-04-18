using System.Linq;
using Machine.Specifications;
using Bifrost.Security;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Security.for_UserSecurityActorExtensions
{
    public class when_user_must_have_claim_type
    {
        static UserSecurityActor    actor;

        Establish context = () => actor = new UserSecurityActor(Mock.Of<ICanResolvePrincipal>());

        Because of = () => actor.MustHaveClaimType("Something");

        It should_add_a_claim_type_rule_to_the_actor = () => actor.Rules.First().ShouldBeOfExactType<ClaimTypeRule>();
        It should_pass_required_claim_type_to_the_rule = () => ((ClaimTypeRule)actor.Rules.First()).ClaimType.ShouldEqual("Something");
    }
}
