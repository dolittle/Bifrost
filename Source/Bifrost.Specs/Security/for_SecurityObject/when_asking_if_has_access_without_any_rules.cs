using Machine.Specifications;
using Bifrost.Security;

namespace Bifrost.Specs.Security.for_SecurityObject
{
    public class when_asking_if_has_access_without_any_rules
    {
        static SecurityObject   security_object;
        static bool result;

        Establish context = () => security_object = new SecurityObject();

        Because of = () => result = security_object.HasAccess("something");

        It should_return_true = () => result.ShouldBeTrue();
    }
}
