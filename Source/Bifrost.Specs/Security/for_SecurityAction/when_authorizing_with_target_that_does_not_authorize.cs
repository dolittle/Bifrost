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
        static AuthorizeTargetResult authorized_target;
        static AuthorizeTargetResult unauthorized_target;
        static AuthorizeActionResult result;

        Establish context = () =>
            {
                var unauthorisedActor = new AuthorizeActorResult(null);
                unauthorisedActor.AddBrokenRule(new Mock<ISecurityRule>().Object);
                var unauthorizedSecurable = new AuthorizeSecurableResult(null);
                unauthorizedSecurable.AddAuthorizeActorResult(unauthorisedActor);

                action = new SecurityAction();
                target_that_authorizes = new Mock<ISecurityTarget>();
                target_that_does_not_authorize = new Mock<ISecurityTarget>();
                authorized_target = new AuthorizeTargetResult(target_that_authorizes.Object);
                unauthorized_target = new AuthorizeTargetResult(target_that_does_not_authorize.Object);
                unauthorized_target.AddAuthorizeSecurableResult(unauthorizedSecurable);

                target_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(authorized_target);
                target_that_does_not_authorize.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(unauthorized_target);

                action.AddTarget(target_that_authorizes.Object);
                action.AddTarget(target_that_does_not_authorize.Object);
            };

        Because of = () => result = action.Authorize(new object());

        It should_not_the_authorized = () => result.IsAuthorized.ShouldBeFalse();
        It should_hold_the_results_of_each_target_authorization = () =>
        {
            result.AuthorizeTargetResults.Count().ShouldEqual(2);
            result.AuthorizeTargetResults.Count(r => r == authorized_target).ShouldEqual(1);
            result.AuthorizeTargetResults.Count(r => r == unauthorized_target).ShouldEqual(1);
        };
        It should_have_a_reference_to_the_action = () => result.Action.ShouldEqual(action);
    }
}