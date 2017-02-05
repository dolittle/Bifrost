using Bifrost.Application;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_Feature
{
    public class when_adding_sub_feature
    {
        static Mock<IModule> module_mock;
        static Feature feature;
        static Mock<ISubFeature> sub_feature_mock;

        Establish context = () =>
        {
            module_mock = new Mock<IModule>();
            feature = new Feature(module_mock.Object, "Some feature");
            sub_feature_mock = new Mock<ISubFeature>();
        };

        Because of = () => feature.AddSubFeature(sub_feature_mock.Object);
        
        It should_contain_the_sub_feature = () => feature.Children.ShouldContainOnly(sub_feature_mock.Object);
    }
}
