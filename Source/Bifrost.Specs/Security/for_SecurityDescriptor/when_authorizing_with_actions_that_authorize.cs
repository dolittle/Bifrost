using System.Linq;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(SecurityDescriptor))]
    public class when_authorizing_with_actions_that_authorize
    {
        static SecurityDescriptor descriptor;
        static Mock<ISecurityAction> action_that_authorizes;
        static Mock<ISecurityAction> another_action_that_authorizes;
        static AuthorizeActionResult authorized_target;
        static AuthorizeActionResult another_authorized_target;
        static AuthorizationResult result;

        Establish context = () =>
        {
            descriptor = new SecurityDescriptor();
            action_that_authorizes = new Mock<ISecurityAction>();
            another_action_that_authorizes = new Mock<ISecurityAction>();
            authorized_target = new AuthorizeActionResult(action_that_authorizes.Object);
            another_authorized_target = new AuthorizeActionResult(another_action_that_authorizes.Object);

            action_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(authorized_target);
            another_action_that_authorizes.Setup(t => t.Authorize(Moq.It.IsAny<object>())).Returns(another_authorized_target);

            descriptor.AddAction(action_that_authorizes.Object);
            descriptor.AddAction(another_action_that_authorizes.Object);
        };

        Because of = () => result = descriptor.Authorize(new object());

        It should_be_authorized = () => result.IsAuthorized.ShouldBeTrue();
        It should_hold_the_results_of_each_action_authorization = () =>
        {
            result.AuthorizeActionResults.Count().ShouldEqual(2);
            result.AuthorizeActionResults.Count(r => r == authorized_target).ShouldEqual(1);
            result.AuthorizeActionResults.Count(r => r == another_authorized_target).ShouldEqual(1);
        };
    }
}