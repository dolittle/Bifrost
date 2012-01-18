using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_two_conventions_and_one_type_and_first_convention_can_resolve : given.a_binding_convention_manager_with_one_type
    {
        static Mock<IBindingConvention> first_convention_mock;
        static Type first_convention_type;

        static FakeBindingConvention second_convention;
        static Type second_convention_type;

        Establish context = () =>
                                {
                                    first_convention_mock = new Mock<IBindingConvention>();
                                    first_convention_type = first_convention_mock.Object.GetType();
                                    manager.Add(first_convention_type);
                                    container_mock.Setup(c => c.Get(first_convention_type)).Returns(first_convention_mock.Object);
                                    first_convention_mock.Setup(c => c.CanResolve(service_type)).Returns(true);

                                    second_convention = new FakeBindingConvention();
                                    second_convention_type = typeof (FakeBindingConvention);
                                    manager.Add(second_convention_type);
                                    container_mock.Setup(c => c.Get(second_convention_type)).Returns(second_convention);
                                    second_convention.CanResolveProperty = false;
                                };

        Because of = () => manager.Initialize();

        It should_not_ask_if_it_can_resolve_on_second_convention = () => second_convention.ResolveCalled.ShouldBeFalse();
    }
}