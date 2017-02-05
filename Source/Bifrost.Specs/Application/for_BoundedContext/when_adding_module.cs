using Bifrost.Application;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_BoundedContext
{
    public class when_adding_module
    {
        static BoundedContext bounded_context;
        static Mock<IModule> module_mock;

        Establish context = () =>
        {
            bounded_context = new BoundedContext("Some bounded context");
            module_mock = new Mock<IModule>();
        };

        Because of = () => bounded_context.AddModule(module_mock.Object);

        It should_add_the_module = () => bounded_context.Children.ShouldContain(module_mock.Object);
    }
}
