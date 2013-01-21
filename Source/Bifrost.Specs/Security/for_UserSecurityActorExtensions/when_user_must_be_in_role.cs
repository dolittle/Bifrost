using System.Linq;
using Machine.Specifications;
using Bifrost.Security;


namespace Bifrost.Specs.Security.for_UserSecurityActorExtensions
{
    public class when_user_must_be_in_role
    {
        static UserSecurityActor    actor;

        Establish context = () => actor = new UserSecurityActor();

        Because of = () => actor.MustBeInRole("Something");

        It should_add_a_role_rule_to_the_actor = () => actor.Rules.First().ShouldBeOfType<RoleRule>();
        It should_pass_required_role_to_the_rule = () => ((RoleRule)actor.Rules.First()).Role.ShouldEqual("Something");
    }
}
