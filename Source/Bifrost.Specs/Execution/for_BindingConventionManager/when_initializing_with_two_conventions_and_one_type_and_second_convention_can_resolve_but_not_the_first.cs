using System;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class when_initializing_with_two_conventions_and_one_type_and_second_convention_can_resolve_but_not_the_first : given.a_binding_convention_manager_with_one_type
    {
        static Type first_convention_type;

        static FakeBindingConvention second_convention;
        static Type second_convention_type;

        Establish context = () =>
        {
            first_convention_type = Get<IBindingConvention>().GetType();
            manager.Add(first_convention_type);
            GetMock<IContainer>().Setup(c => c.Get(first_convention_type)).Returns(Get<IBindingConvention>());
            GetMock<IBindingConvention>().Setup(c => c.CanResolve(Get<IContainer>(), service_type)).Returns(false);

            second_convention = new FakeBindingConvention();
            second_convention_type = typeof (FakeBindingConvention);
            manager.Add(second_convention_type);
            GetMock<IContainer>().Setup(c => c.Get(second_convention_type)).Returns(second_convention);
            second_convention.CanResolveProperty = true;
        };

        Because of = () => manager.Initialize();

        It should_not_resolve_on_first_convention = () => GetMock<IBindingConvention>().Verify(c => c.Resolve(Get<IContainer>(), service_type), Times.Never());
        It should_ask_if_it_can_resolve_on_second_convention = () => second_convention.CanResolveCalled.ShouldBeTrue();
        It should_resolve_on_second_convention = () => second_convention.ResolveCalled.ShouldBeTrue();
    }
}