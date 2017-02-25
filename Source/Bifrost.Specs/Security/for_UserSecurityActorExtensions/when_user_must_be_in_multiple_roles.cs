using System.Linq;
using Machine.Specifications;
using Bifrost.Security;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Security.for_UserSecurityActorExtensions
{
    public class when_user_must_be_in_multiple_roles
    {
        static UserSecurityActor    actor;

        Establish context = () => actor = new UserSecurityActor(Mock.Of<ICanResolvePrincipal>());

        Because of = () => actor.MustBeInRoles("Something","SomethingElse");

        It should_add_only_role_rules_to_the_actor = () => actor.Rules.All(c => c.GetType() == typeof(RoleRule)).ShouldBeTrue();
        It should_add_two_role_rules_to_the_actor = () => actor.Rules.Count(c => c.GetType() == typeof(RoleRule)).ShouldEqual(2);
        It should_pass_required_role_to_the_rule = () => ((RoleRule)actor.Rules.First()).Role.ShouldEqual("Something");
    }
}
