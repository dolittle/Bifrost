using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Security.for_SecurityTarget
{
    public class when_adding_a_securable
    {
        static SecurityTarget security_target;
        static Mock<ISecurable> securable_mock;

        Establish context = () => 
        {
            security_target = new SecurityTarget();
            securable_mock = new Mock<ISecurable>();
        };

        Because of = () => security_target.AddSecurable(securable_mock.Object);

        It should_have_it_available_in_the_collection = () => security_target.Securables.ShouldContain(securable_mock.Object);
    }
}
