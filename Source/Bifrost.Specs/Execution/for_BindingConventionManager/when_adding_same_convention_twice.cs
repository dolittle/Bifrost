using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_adding_same_convention_twice : given.a_binding_convention_manager_with_one_type
    {
        static Mock<IBindingConvention> convention_mock;
        static Type convention_type;

        Establish context = () =>
                                {
                                    convention_mock = new Mock<IBindingConvention>();
            
                                    convention_type = convention_mock.Object.GetType();
                                    container_mock.Setup(c => c.Get(convention_type)).Returns(convention_mock.Object);
                                };

        Because of = () =>
                         {
                             manager.Add(convention_type);
                             manager.Add(convention_type);
                             manager.Initialize();
                         };

        It should_not_try_to_resolve_twice = () => convention_mock.Verify(c => c.CanResolve(container_mock.Object, service_type), Times.Once());
    }
}