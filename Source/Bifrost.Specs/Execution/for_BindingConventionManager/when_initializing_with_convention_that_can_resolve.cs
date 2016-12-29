using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_one_convention : given.a_binding_convention_manager
    {
        static Type convention_type;
        
        Establish context = () =>
        {
            convention_type = Get<IBindingConvention>().GetType();
            manager.Add(convention_type);
        };
        Because of = () => manager.Initialize();

        It should_get_an_instance_of_the_convention = () => GetMock<IContainer>().Verify(c => c.Get(convention_type), Moq.Times.Once());
    }
}
