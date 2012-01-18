using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;


namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_one_convention_and_one_type : given.a_binding_convention_manager_with_one_type
    {
        static Mock<IBindingConvention> convention_mock;
        static Type convention_type;

        Establish context = () =>
                                {
                                    convention_mock = new Mock<IBindingConvention>();
                                    convention_type = convention_mock.Object.GetType();
                                    manager.Add(convention_type);
                                    container_mock.Setup(c => c.Get(convention_type)).Returns(convention_mock.Object);
                                    convention_mock.Setup(c => c.CanResolve(service_type)).Returns(true);
                                };

        Because of = () => manager.Initialize();

        It should_resolve = () => convention_mock.Verify(c => c.Resolve(container_mock.Object, service_type));
    }
}