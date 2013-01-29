using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityAction
{
    [Subject(typeof(SecurityAction))]
    public class when_authorizing_with_target_that_does_not_authorize
    {
        static SecurityAction action;
        static Mock<ISecurityTarget> target_that_authorizes;
        static Mock<ISecurityTarget> target_that_does_not_authorize;
        static Mock<ISecurityTarget> target_that_cannot_authorize;
        static AuthorizeTargetResult authorized_target;
        static AuthorizeTargetResult unauthorized_target;
        static AuthorizeActionResult result;

        Establish context = () =>
            {
                var unauthorisedActor = new AuthorizeActorResult(null);
                unauthorisedActor.AddBrokenRule(new Mock<ISecurityRule>().Object);
                var unauthorizedSecurable = new AuthorizeSecurableResult(null);
                unauthorizedSecurable.ProcessAuthorizeActorResult(unauthorisedActor);

                action = new SecurityAction();
                target_that_authorizes = CreateTarget(canAuthorize:true);
                target_that_does_not_authorize = CreateTarget(canAuthorize: true);
                target_that_cannot_authorize = CreateTarget(canAuthorize: false);
                authorized_target = new AuthorizeTargetResult(target_that_authorizes.Object);
                unauthorized_target = new AuthorizeTargetResult(target_that_does_not_authorize.Object);
                unauthorized_target.ProcessAuthorizeSecurableResult(unauthorizedSecurable);

                target_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(authorized_target);
                target_that_does_not_authorize.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(unauthorized_target);

                action.AddTarget(target_that_authorizes.Object);
                action.AddTarget(target_that_does_not_authorize.Object);
                action.AddTarget(target_that_cannot_authorize.Object);
            };

        Because of = () => result = action.Authorize(new object());

        It should_not_the_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_hold_the_results_of_each_failed_target_authorization = () =>
        {
            result.AuthorizationFailures.Count().ShouldEqual(1);
            result.AuthorizationFailures.Count(r => r == unauthorized_target).ShouldEqual(1);
        };
        It should_not_attempt_to_authorize_target_that_cannot_authorize = () => target_that_cannot_authorize.Verify(t => t.Authorize(Moq.It.IsAny<object>()), Times.Never());
        It should_have_a_reference_to_the_action = () => result.Action.ShouldEqual(action);


        static Mock<ISecurityTarget> CreateTarget(bool canAuthorize)
        {
            var mock = new Mock<ISecurityTarget>();
            mock.Setup(m => m.CanAuthorize(Moq.It.IsAny<object>())).Returns(canAuthorize);
            return mock;
        }
    }
}