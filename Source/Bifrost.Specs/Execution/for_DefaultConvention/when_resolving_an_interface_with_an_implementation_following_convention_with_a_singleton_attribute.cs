using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_DefaultConvention
{
    [Subject(typeof(DefaultConvention))]
    public class when_resolving_an_interface_with_an_implementation_following_convention_with_a_singleton_attribute
    {
        static Mock<IContainer> container_mock;
        static DefaultConvention convention;

        Establish context = () => {
            container_mock = new Mock<IContainer>();
            convention = new DefaultConvention();
        };

        Because of = () => convention.Resolve(container_mock.Object, typeof(ISomething));

        It should_bind_with_singleton_lifecycle = () => container_mock.Verify(c => c.Bind(typeof(ISomething), typeof(Something), BindingLifecycle.Singleton));
    }
}
