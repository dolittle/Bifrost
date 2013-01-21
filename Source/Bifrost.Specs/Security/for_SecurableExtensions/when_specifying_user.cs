using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurableExtensions
{
    public class when_specifying_user
    {
        static TypeSecurable securable;
        static ISecurityActor actor;

        Establish context = () => securable = new TypeSecurable(typeof(object));

        Because of = () => actor = securable.User();

        It should_return_an_user_actor_builder = () => actor.ShouldBeOfType<UserSecurityActor>();
        It should_add_actor_to_securable = () => securable.Actors.First().ShouldBeOfType<UserSecurityActor>();
    }
}
