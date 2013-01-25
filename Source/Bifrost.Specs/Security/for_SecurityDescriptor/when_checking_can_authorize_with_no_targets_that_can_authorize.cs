using Bifrost.Security;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_checking_can_authorize_with_no_targets_that_can_authorize
    {
        static ISecurityAction action_that_cannot_authorize;
        static ISecurityAction another_action_that_cannot_authorize;
        static BaseSecurityDescriptor descriptor;
        static bool can_authorize;

        public when_checking_can_authorize_with_no_targets_that_can_authorize()
        {
            action_that_cannot_authorize = new MySecurityAction(o => false,o => null);
            another_action_that_cannot_authorize = new MySecurityAction(o => false,o => null);

            descriptor = new BaseSecurityDescriptor();
            descriptor.AddAction(another_action_that_cannot_authorize);
            descriptor.AddAction(action_that_cannot_authorize);
        }

        Because of = () => can_authorize = descriptor.CanAuthorize<MySecurityAction>(new object());

        It should_be_authorizable = () => can_authorize.ShouldBeFalse();
    }
}