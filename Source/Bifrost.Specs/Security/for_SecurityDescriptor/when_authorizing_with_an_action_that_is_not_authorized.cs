using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_with_an_action_that_is_not_authorized
    {
        static BaseSecurityDescriptor descriptor;
        static Mock<ISecurityAction> action_that_authorizes;
        static Mock<ISecurityAction> action_that_does_not_authorize;
        static Mock<ISecurityAction> action_that_cannot_authorize;
        static AuthorizeActionResult authorized_target;
        static AuthorizeActionResult unauthorized_target;
        static AuthorizationResult result;

        Establish context = () =>
            {
                var unauthorized_actor_result = new AuthorizeActorResult(null);
                unauthorized_actor_result.AddBrokenRule(new Mock<ISecurityRule>().Object);
                var unauthorized_securable_result = new AuthorizeSecurableResult(null);
                unauthorized_securable_result.AddAuthorizeActorResult(unauthorized_actor_result);
                var unauthorized_target_result = new AuthorizeTargetResult(null);
                unauthorized_target_result.AddAuthorizeSecurableResult(unauthorized_securable_result);

                descriptor = new BaseSecurityDescriptor();
                action_that_authorizes = new Mock<ISecurityAction>();
                action_that_does_not_authorize = new Mock<ISecurityAction>();
                action_that_cannot_authorize = new Mock<ISecurityAction>();
                authorized_target = new AuthorizeActionResult(action_that_authorizes.Object);
                unauthorized_target = new AuthorizeActionResult(action_that_does_not_authorize.Object);
                unauthorized_target.AddAuthorizeTargetResult(unauthorized_target_result);

                action_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(authorized_target);
                action_that_authorizes.Setup(t => t.CanAuthorize(Moq.It.IsAny<object>())).Returns(true);
                action_that_does_not_authorize.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(unauthorized_target);
                action_that_does_not_authorize.Setup(t => t.CanAuthorize(Moq.It.IsAny<object>())).Returns(true);
                action_that_cannot_authorize.Setup(t => t.CanAuthorize(Moq.It.IsAny<object>())).Returns(false);

                descriptor.AddAction(action_that_authorizes.Object);
                descriptor.AddAction(action_that_does_not_authorize.Object);
                descriptor.AddAction(action_that_cannot_authorize.Object);
            };

        Because of = () => result = descriptor.Authorize(new object());

        It should_not_be_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_hold_the_results_of_each_failed_action_authorization = () =>
            {
                result.AuthorizeActionResults.Count().ShouldEqual(1);
                result.AuthorizeActionResults.All(r => r == unauthorized_target);
            };
        It should_not_attempt_to_authorize_action_that_cannot_authorize = () => action_that_cannot_authorize.Verify(a => a.Authorize(Moq.It.IsAny<object>()), Times.Never());
    }
}