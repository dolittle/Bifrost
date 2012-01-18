using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_one_convention : given.a_binding_convention_manager
    {
        static Type convention_type;
        
        Establish context = () =>
                                {
                                    var convention_mock = new Mock<IBindingConvention>();
                                    convention_type = convention_mock.Object.GetType();
                                    manager.Add(convention_type);
                                };
        Because of = () => manager.Initialize();

        It should_get_an_instance_of_the_convention = () => container_mock.Verify(c => c.Get(convention_type), Times.Once());
    }
}
