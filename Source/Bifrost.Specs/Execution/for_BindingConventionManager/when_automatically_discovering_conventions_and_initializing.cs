using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_automatically_discovering_conventions_and_initializing : given.a_binding_convention_manager_with_one_type
    {
        static Type convention_type;

        Establish context = () =>
        {
            GetMock<IBindingConvention>().Setup(c => c.CanResolve(Get<IContainer>(), service_type)).Returns(true);
            convention_type = Get<IBindingConvention>().GetType();
            GetMock<IContainer>().Setup(c => c.Get(convention_type)).Returns(Get<IBindingConvention>());

            GetMock<ITypeDiscoverer>().Setup(t => t.FindMultiple<IBindingConvention>()).Returns(new[] {convention_type});
        };

        Because of = () => manager.DiscoverAndInitialize();

        It should_ask_if_convention_can_resolve = () => GetMock<IBindingConvention>().Verify(c => c.CanResolve(Get<IContainer>(), service_type), Moq.Times.Once());
    }
}