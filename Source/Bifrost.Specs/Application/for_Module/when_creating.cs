using Bifrost.Application;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_Module
{
    public class when_creating
    {
        const string name = "Some Module";
        static Mock<IBoundedContext> bounded_context_mock;
        static Module module;

        Establish context = () => bounded_context_mock = new Mock<IBoundedContext>();

        Because of = () => module = new Module(bounded_context_mock.Object, "Some Module");

        It should_set_the_name = () => ((string) module.Name).ShouldEqual(name);
        It should_set_the_bounded_context = () => module.Parent.ShouldEqual(bounded_context_mock.Object);
        It should_add_itself_to_the_bounded_context = () => bounded_context_mock.Verify(b => b.AddModule(module), Times.Once());
    }
}
