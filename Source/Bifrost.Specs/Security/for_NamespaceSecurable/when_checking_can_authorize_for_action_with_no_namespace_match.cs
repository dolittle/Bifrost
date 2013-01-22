using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_NamespaceSecurable
{
    [Subject(typeof(NamespaceSecurable))]
    public class when_checking_can_authorize_for_action_with_no_namespace_match : given.a_namespace_securable
    {
        static bool can_authorize;

        Because of = () => can_authorize = namespace_securable.CanAuthorize(action_within_another_namespace);

        It should_not_be_authorizable = () => can_authorize.ShouldBeFalse();
    }
}