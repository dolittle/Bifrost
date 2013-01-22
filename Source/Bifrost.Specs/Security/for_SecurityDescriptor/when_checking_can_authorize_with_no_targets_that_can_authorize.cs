using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(SecurityDescriptor))]
    public class when_checking_can_authorize_with_no_targets_that_can_authorize
    {
        static Mock<ISecurityAction> action_that_cannot_authorize;
        static Mock<ISecurityAction> another_action_that_cannot_authorize;
        static SecurityDescriptor descriptor;
        static bool can_authorize;

        public when_checking_can_authorize_with_no_targets_that_can_authorize()
        {
            action_that_cannot_authorize = new Mock<ISecurityAction>();
            action_that_cannot_authorize.Setup(a => a.CanAuthorize(Moq.It.IsAny<object>())).Returns(false);
            another_action_that_cannot_authorize = new Mock<ISecurityAction>();
            another_action_that_cannot_authorize.Setup(a => a.CanAuthorize(Moq.It.IsAny<object>())).Returns(false);

            descriptor = new SecurityDescriptor();
            descriptor.AddAction(another_action_that_cannot_authorize.Object);
            descriptor.AddAction(action_that_cannot_authorize.Object);
        }

        Because of = () => can_authorize = descriptor.CanAuthorize(new object());

        It should_be_authorizable = () => can_authorize.ShouldBeFalse();
    }
}