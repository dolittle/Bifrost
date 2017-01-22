using Bifrost.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_SubFeature
{
    public class when_adding_sub_feature
    {
        static Mock<IFeature> parent_feature_mock;
        static SubFeature feature;
        static Mock<ISubFeature> sub_feature_mock;

        Establish context = () =>
        {
            parent_feature_mock = new Mock<IFeature>();
            feature = new SubFeature(parent_feature_mock.Object, "Some feature");
            sub_feature_mock= new Mock<ISubFeature>();
        };

        Because of = () => feature.AddSubFeature(sub_feature_mock.Object);
        
        It should_contain_the_sub_feature = () => feature.SubFeatures.ShouldContainOnly(sub_feature_mock.Object);
    }
}
