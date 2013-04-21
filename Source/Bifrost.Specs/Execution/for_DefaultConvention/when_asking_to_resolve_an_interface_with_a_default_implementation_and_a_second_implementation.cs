using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Bifrost.Execution;

namespace Bifrost.Specs.Execution.for_DefaultConvention
{
    [Subject(typeof(DefaultConvention))]
    public class when_asking_to_resolve_an_interface_with_a_default_implementation_and_a_second_implementation
    {
        static Mock<IContainer>  container_mock = new Mock<IContainer>();
        static DefaultConvention convention = new DefaultConvention();
        static bool result = true;

        Establish context = () => 
        {
            container_mock.Setup(c => c.HasBindingFor(typeof(ISomethingWithMultipleImplementations))).Returns(false);
            container_mock.Setup(c => c.HasBindingFor(typeof(SomethingWithMultipleImplementations))).Returns(false);
            container_mock.Setup(c => c.HasBindingFor(typeof(SomethingWithMultipleImplementationsSecond))).Returns(false);
        };

        Because of = () => result = convention.CanResolve(container_mock.Object, typeof(ISomethingWithMultipleImplementations));

        It should_return_false = () => result.ShouldBeFalse();
    }
}
