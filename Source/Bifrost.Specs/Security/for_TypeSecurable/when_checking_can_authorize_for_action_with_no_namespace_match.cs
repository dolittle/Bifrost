using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_TypeSecurable
{
    [Subject(typeof(NamespaceSecurable))]
    public class when_checking_can_authorize_for_action_of_another_type : given.a_type_securable
    {
        static bool can_authorize;

        Because of = () => can_authorize = type_securable.CanAuthorize(action_of_another_type);

        It should_not_be_authorizable = () => can_authorize.ShouldBeFalse();
    }
}