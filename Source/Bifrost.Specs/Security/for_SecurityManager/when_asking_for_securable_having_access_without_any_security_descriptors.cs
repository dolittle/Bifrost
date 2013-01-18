using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityManager
{
    public class when_asking_for_securable_having_access_without_any_security_descriptors : given.a_security_manager
    {
        const string securable = "something";

        static bool result;

        Because of = () => result = security_manager.HasAccess(securable);

        It should_return_true = () => result.ShouldBeTrue();
    }
}
