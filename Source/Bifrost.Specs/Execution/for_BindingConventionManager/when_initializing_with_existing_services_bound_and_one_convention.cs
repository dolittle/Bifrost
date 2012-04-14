using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_existing_services_bound_and_one_convention : given.a_binding_convention_manager_with_one_type
    {
        static Mock<IBindingConvention> convention_mock;
        static Type convention_type;
        

        Establish context = () =>
                                {
                                    convention_mock = new Mock<IBindingConvention>();
                                    convention_type = convention_mock.Object.GetType();
                                    container_mock.Setup(c => c.GetBoundServices()).Returns(new[] {service_type});
                                    container_mock.Setup(c => c.Get(convention_type)).Returns(convention_mock.Object);
                                    manager.Add(convention_type);
                                };
        Because of = () => manager.Initialize();

        It should_not_ask_to_resolve = () => convention_mock.Verify(c => c.CanResolve(container_mock.Object, service_type), Times.Never());
    }
}