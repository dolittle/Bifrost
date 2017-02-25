using System.Linq;
using Bifrost.Configuration;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurableExtensions
{
    public class when_specifying_user
    {
        static TypeSecurable securable;
        static ISecurityActor actor;

        Establish context = () =>
        {
            securable = new TypeSecurable(typeof(object));
            Configure.With(Mock.Of<IContainer>(m => m.Get<ICanResolvePrincipal>() == Mock.Of<ICanResolvePrincipal>()), (IDefaultConventions) null, null, null);
        };

        Because of = () => actor = securable.User();

        It should_return_an_user_actor_builder = () => actor.ShouldBeOfExactType<UserSecurityActor>();
        It should_add_actor_to_securable = () => securable.Actors.First().ShouldBeOfExactType<UserSecurityActor>();
    }
}
