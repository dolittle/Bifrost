using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityTarget
{
    [Subject(typeof(SecurityTarget))]
    public class when_checking_can_authorize_with_a_securable_that_can_authorize
    {
        static Mock<ISecurable> securable_that_can_authorize;
        static Mock<ISecurable> securable_that_cannot_authorize;
        static SecurityTarget target;
        static bool can_authorize;

        Establish context = () =>
            {
                securable_that_can_authorize = new Mock<ISecurable>();
                securable_that_can_authorize.Setup(s => s.CanAuthorize(Moq.It.IsAny<object>())).Returns(true);
                securable_that_cannot_authorize = new Mock<ISecurable>();
                securable_that_cannot_authorize.Setup(s => s.CanAuthorize(Moq.It.IsAny<object>())).Returns(false);
                target = new SecurityTarget();
                target.AddSecurable(securable_that_cannot_authorize.Object);
                target.AddSecurable(securable_that_can_authorize.Object);
            };

        Because of = () => can_authorize = target.CanAuthorize(new object());

        It should_be_authorizable = () => can_authorize.ShouldBeTrue();
    }
}