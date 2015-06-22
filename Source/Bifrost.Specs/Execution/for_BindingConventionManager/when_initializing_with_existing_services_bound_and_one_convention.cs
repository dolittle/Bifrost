using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_existing_services_bound_and_one_convention : given.a_binding_convention_manager_with_one_type
    {
        static Type convention_type;
        

        Establish context = () =>
        {
            convention_type = Get<IBindingConvention>().GetType();
            GetMock<IContainer>().Setup(c => c.GetBoundServices()).Returns(new[] {service_type});
            GetMock<IContainer>().Setup(c => c.Get(convention_type)).Returns(Get<IBindingConvention>());
            manager.Add(convention_type);
        };
        Because of = () => manager.Initialize();

        It should_not_ask_to_resolve = () => GetMock<IBindingConvention>().Verify(c => c.CanResolve(Get<IContainer>(), service_type), Moq.Times.Never());
    }
}