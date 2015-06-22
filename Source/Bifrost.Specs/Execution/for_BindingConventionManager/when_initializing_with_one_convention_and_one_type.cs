using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_one_convention_and_one_type : given.a_binding_convention_manager_with_one_type
    {
        static Type convention_type;

        Establish context = () =>
        {
            convention_type = Get<IBindingConvention>().GetType();
            manager.Add(convention_type);
            GetMock<IContainer>().Setup(c => c.Get(convention_type)).Returns(Get<IBindingConvention>());
            GetMock<IBindingConvention>().Setup(c => c.CanResolve(Get<IContainer>(), service_type)).Returns(true);
        };

        Because of = () => manager.Initialize();

        It should_resolve = () => GetMock<IBindingConvention>().Verify(c => c.Resolve(Get<IContainer>(), service_type));
    }
}