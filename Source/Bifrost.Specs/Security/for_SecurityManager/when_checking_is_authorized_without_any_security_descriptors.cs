using Bifrost.Security;
using Machine.Specifications;
using Bifrost.Commands;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityManager
{
    [Subject(typeof(SecurityManager))]
    public class when_checking_is_authorized_without_any_security_descriptors : given.a_security_manager_with_no_descriptors
    {
        const string securable = "something";

        static bool result;

        Because of = () => result = security_manager.IsAuthorized<HandleCommand>(securable);

        It should_return_true = () => result.ShouldBeTrue();
    }
}
