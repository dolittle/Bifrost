using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_adding_same_convention_twice : given.a_binding_convention_manager_with_one_type
    {
        static Type convention_type;

        Establish context = () =>
        {
            convention_type = Get<IBindingConvention>().GetType();
            GetMock<IContainer>().Setup(c => c.Get(convention_type)).Returns(Get<IBindingConvention>());
        };

        Because of = () =>
        {
            manager.Add(convention_type);
            manager.Add(convention_type);
            manager.Initialize();
        };

        It should_not_try_to_resolve_twice = () => GetMock<IBindingConvention>().Verify(c => c.CanResolve(Get<IContainer>(), service_type), Moq.Times.Once());
    }
}