using System.Linq;
using Machine.Specifications;
using Bifrost.Security;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Security.for_UserSecurityActorExtensions
{
    public class when_user_must_have_multiple_claim_types
    {
        const string first_claim_type = "Something";
        const string second_claim_type = "SomethingElse";
        static UserSecurityActor actor;

        Establish context = () => actor = new UserSecurityActor(Mock.Of<ICanResolvePrincipal>());

        Because of = () => actor.MustHaveClaimTypes(first_claim_type, second_claim_type);

        It should_add_only_claim_type_rules_to_the_actor = () => actor.Rules.All(c => c.GetType() == typeof(ClaimTypeRule)).ShouldBeTrue();
        It should_add_two_claim_type_rules_to_the_actor = () => actor.Rules.Count(c => c.GetType() == typeof(ClaimTypeRule)).ShouldEqual(2);
        It should_pass_required_claim_type_to_the_first_rule = () => ((ClaimTypeRule)actor.Rules.ToArray()[0]).ClaimType.ShouldEqual(first_claim_type);
        It should_pass_required_claim_type_to_the_second_rule = () => ((ClaimTypeRule)actor.Rules.ToArray()[1]).ClaimType.ShouldEqual(second_claim_type);
    }
}
