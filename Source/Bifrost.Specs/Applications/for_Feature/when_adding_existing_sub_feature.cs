using System;
using Bifrost.Applications;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_Feature
{
    public class when_adding_existing_sub_feature
    {
        static Mock<IModule> module_mock;
        static Feature feature;
        static Mock<ISubFeature> sub_feature_mock;
        static Exception exception;

        Establish context = () =>
        {
            module_mock = new Mock<IModule>();
            feature = new Feature(module_mock.Object, "Some feature");
            sub_feature_mock = new Mock<ISubFeature>();
            feature.AddSubFeature(sub_feature_mock.Object);
        };

        Because of = () => exception = Catch.Exception(() => feature.AddSubFeature(sub_feature_mock.Object));

        It should_throw_sub_feature_already_added_to_feature = () => exception.ShouldBeOfExactType<SubFeatureAlreadyAddedToFeature>();
    }
}
