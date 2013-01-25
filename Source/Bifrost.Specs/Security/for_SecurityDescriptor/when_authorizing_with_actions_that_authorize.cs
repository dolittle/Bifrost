using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_with_actions_that_authorize
    {
        static BaseSecurityDescriptor descriptor;
        static Mock<ISecurityAction> action_that_authorizes;
        static Mock<ISecurityAction> another_action_that_authorizes;
        static Mock<ISecurityAction> action_that_cannot_authorize;
        static AuthorizeActionResult authorized_target;
        static AuthorizeActionResult another_authorized_target;
        static AuthorizationResult result;

        Establish context = () =>
        {
            descriptor = new BaseSecurityDescriptor();
            action_that_authorizes = new Mock<ISecurityAction>();
            another_action_that_authorizes = new Mock<ISecurityAction>();
            action_that_cannot_authorize = new Mock<ISecurityAction>();
            authorized_target = new AuthorizeActionResult(action_that_authorizes.Object);
            another_authorized_target = new AuthorizeActionResult(another_action_that_authorizes.Object);

            action_that_authorizes.Setup(t => t.CanAuthorize(Moq.It.IsAny<object>())).Returns(true);
            action_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(authorized_target);
            another_action_that_authorizes.Setup(t => t.CanAuthorize(Moq.It.IsAny<object>())).Returns(true);
            another_action_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(another_authorized_target);
            action_that_cannot_authorize.Setup(t => t.CanAuthorize(Moq.It.IsAny<object>())).Returns(false);

            descriptor.AddAction(action_that_authorizes.Object);
            descriptor.AddAction(another_action_that_authorizes.Object);
            descriptor.AddAction(action_that_cannot_authorize.Object);
        };

        Because of = () => result = descriptor.Authorize(new object());

        It should_be_authorized = () => result.IsAuthorized.ShouldBeTrue();
        It should_not_have_any_failed_action_authorizations = () => result.AuthorizeActionResults.Any().ShouldBeFalse();
        It should_not_attempt_to_authorize_action_that_cannot_authorize = () => action_that_cannot_authorize.Verify(a => a.Authorize(Moq.It.IsAny<object>()), Times.Never());
    }
}