using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_automatically_discovering_conventions_and_initializing : given.a_binding_convention_manager_with_one_type
    {
        static Mock<IBindingConvention> convention_mock;
        static Type convention_type;

        Establish context = () =>
                                {
                                    convention_mock = new Mock<IBindingConvention>();
                                    convention_mock.Setup(c => c.CanResolve(container_mock.Object, service_type)).Returns(true);
                                    convention_type = convention_mock.Object.GetType();
                                    container_mock.Setup(c => c.Get(convention_type)).Returns(convention_mock.Object);

                                    type_discoverer_mock.Setup(t => t.FindMultiple<IBindingConvention>()).Returns(new[] {convention_type});
                                };

        Because of = () => manager.DiscoverAndInitialize();

        It should_ask_if_convention_can_resolve = () => convention_mock.Verify(c => c.CanResolve(container_mock.Object, service_type), Times.Once());
    }
}