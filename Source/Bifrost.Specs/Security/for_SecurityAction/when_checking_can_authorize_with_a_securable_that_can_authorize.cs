using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityAction
{
    [Subject(typeof(SecurityAction))]
    public class when_checking_can_authorize_with_a_target_that_can_authorize
    {
        static Mock<ISecurityTarget> target_that_can_authorize;
        static Mock<ISecurityTarget> target_that_cannot_authorize;
        static SecurityAction action;
        static bool can_authorize;

        Establish context = () =>
            {
                target_that_can_authorize = new Mock<ISecurityTarget>();
                target_that_can_authorize.Setup(s => s.CanAuthorize(Moq.It.IsAny<object>())).Returns(true);
                target_that_cannot_authorize = new Mock<ISecurityTarget>();
                target_that_cannot_authorize.Setup(s => s.CanAuthorize(Moq.It.IsAny<object>())).Returns(false);
                action = new SecurityAction();
                action.AddTarget(target_that_cannot_authorize.Object);
                action.AddTarget(target_that_can_authorize.Object);
            };

        Because of = () => can_authorize = action.CanAuthorize(new object());

        It should_be_authorizable = () => can_authorize.ShouldBeTrue();
    }
}