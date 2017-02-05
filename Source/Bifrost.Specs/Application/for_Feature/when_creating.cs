using Bifrost.Application;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_Feature
{
    public class when_creating
    {
        const string name = "Some Feature";
        static Mock<IModule> module_mock;
        static Feature feature;

        Because of = () =>
        {
            module_mock = new Mock<IModule>();
            feature = new Feature(module_mock.Object, name);
        };

        It should_set_the_name = () => ((string) feature.Name).ShouldEqual(name);
        It should_set_module = () => feature.Parent.ShouldEqual(module_mock.Object);
        It should_add_the_itself_to_the_module = () => module_mock.Verify(m => m.AddFeature(feature), Times.Once());
    }
}
