using Machine.Specifications;
using Bifrost.Security;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    public class when_adding_an_action
    {
        static BaseSecurityDescriptor security_descriptor;
        static Mock<ISecurityAction> security_action_mock;

        Establish context = () => 
        {
            security_descriptor = new BaseSecurityDescriptor();
            security_action_mock = new Mock<ISecurityAction>();
        };

        Because of = () => security_descriptor.AddAction(security_action_mock.Object);

        It should_have_it_available_as_action_from_the_enumerable = () => security_descriptor.Actions.ShouldContain(security_action_mock.Object);
    }
}
