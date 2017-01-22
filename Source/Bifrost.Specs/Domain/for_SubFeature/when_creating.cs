using Bifrost.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_SubFeature
{
    public class when_creating
    {
        const string name = "Sub Feature";
        static Mock<IModule> module_mock;
        static Mock<IFeature> parent_feature_mock;
        static SubFeature sub_feature;

        Establish context = () =>
        {
            module_mock = new Mock<IModule>();
            parent_feature_mock = new Mock<IFeature>();
            parent_feature_mock.Setup(p => p.Module).Returns(module_mock.Object);
        };

        Because of = () => sub_feature = new SubFeature(parent_feature_mock.Object, name);

        It should_set_the_name = () => ((string) sub_feature.Name).ShouldEqual(name);
        It should_set_the_module = () => sub_feature.Module.ShouldEqual(module_mock.Object);
        It should_add_itself_to_the_parent = () => parent_feature_mock.Verify(p => p.AddSubFeature(sub_feature), Times.Once());
    }
}
