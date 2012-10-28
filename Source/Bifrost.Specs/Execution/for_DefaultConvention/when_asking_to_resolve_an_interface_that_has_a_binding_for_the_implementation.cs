using Bifrost.Execution;
using Machine.Specifications;
using Moq;
using System;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_DefaultConvention
{
    [Subject(typeof(DefaultConvention))]
    public class when_asking_to_resolve_an_interface_that_has_a_binding_for_the_implementation
    {
        static Mock<IContainer>  container_mock = new Mock<IContainer>();
        static DefaultConvention convention = new DefaultConvention();
        static bool result = true;
             
        Establish context = () => container_mock.Setup(c => c.HasBindingFor(typeof(Something))).Returns(true);

        Because of = () => result = convention.CanResolve(container_mock.Object, typeof(ISomething));

        It should_return_false = () => result.ShouldBeFalse();
    }
}
