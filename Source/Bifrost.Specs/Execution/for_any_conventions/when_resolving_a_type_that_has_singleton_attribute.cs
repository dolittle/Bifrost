using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using System;

namespace Bifrost.Specs.Execution.for_any_conventions
{
    public class when_resolving_a_type_that_has_singleton_attribute
    {
        static Mock<IContainer> container_mock;
        static SelfBindingConvention    convention;
        static Type type = typeof(TypeWithSingletonAttribute);

        Establish context = () => {
            container_mock = new Mock<IContainer>();
            convention = new SelfBindingConvention();
        };

        Because of = () => convention.Resolve(container_mock.Object, type);

        It should_bind_with_singleton_lifecycle = () => container_mock.Verify(c => c.Bind(type, type, BindingLifecycle.Singleton));
    }
}
