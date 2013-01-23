using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityAction
{
    [Subject(typeof(SecurityAction))]
    public class when_authorizing_with_targets_that_all_authorize
    {
        static SecurityAction action;
        static Mock<ISecurityTarget> target_that_authorizes;
        static Mock<ISecurityTarget> another_target_that_authorizes;
        static AuthorizeTargetResult authorized_target;
        static AuthorizeTargetResult another_authorized_target;
        static AuthorizeActionResult result;

        Establish context = () =>
            {
                action = new SecurityAction();
                target_that_authorizes = new Mock<ISecurityTarget>();
                another_target_that_authorizes = new Mock<ISecurityTarget>();
                authorized_target = new AuthorizeTargetResult(target_that_authorizes.Object);
                another_authorized_target = new AuthorizeTargetResult(another_target_that_authorizes.Object);

                target_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(authorized_target);
                another_target_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(another_authorized_target);

                action.AddTarget(target_that_authorizes.Object);
                action.AddTarget(another_target_that_authorizes.Object);
            };

        Because of = () => result = action.Authorize(new object());

        It should_be_the_authorized = () => result.IsAuthorized.ShouldBeTrue();
        It should_not_have_any_failed_authorizations = () => result.AuthorizeTargetResults.Any().ShouldBeFalse();
        It should_have_a_reference_to_the_action = () => result.Action.ShouldEqual(action);
    }
}