using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurableBuilderExtensions
{
    public class when_specifying_user
    {
        static SecurableBuilder<TypeSecurable>  securable_builder;
        static ISecurityActor actor;

        Establish context = () => securable_builder = new SecurableBuilder<TypeSecurable>(new TypeSecurable(typeof(object)));

        Because of = () => actor = securable_builder.User();

        It should_return_an_user_actor_builder = () => actor.ShouldBeOfType<UserSecurityActor>();
        It should_add_actor_to_securable = () => securable_builder.Securable.Actors.First().ShouldBeOfType<UserSecurityActor>();
    }
}
