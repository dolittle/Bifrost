using System;
using Bifrost.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_Module
{
    public class when_adding_existing_feature
    {
        static Mock<IBoundedContext> bounded_context_mock;
        static Module module;
        static Mock<IFeature> feature_mock;
        static Exception exception;

        Establish context = () =>
        {
            bounded_context_mock = new Mock<IBoundedContext>();
            module = new Module(bounded_context_mock.Object, "Some Module");
            feature_mock = new Mock<IFeature>();
            module.AddFeature(feature_mock.Object);
        };

        Because of = () => exception = Catch.Exception(() => module.AddFeature(feature_mock.Object));

        It should_throw_feature_already_added_to_module = () => exception.ShouldBeOfExactType<FeatureAlreadyAddedToModule>();
    }
}
