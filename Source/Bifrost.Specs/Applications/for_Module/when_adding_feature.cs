using Bifrost.Applications;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_Module
{
    public class when_adding_feature
    {
        static Mock<IBoundedContext> bounded_context_mock;
        static Module module;
        static Mock<IFeature> feature_mock;

        Establish context = () =>
        {
            bounded_context_mock = new Mock<IBoundedContext>();
            module = new Module(bounded_context_mock.Object, "Some Module");
            feature_mock = new Mock<IFeature>();
        };

        Because of = () => module.AddFeature(feature_mock.Object);

        It should_contain_the_feature = () => module.Children.ShouldContainOnly(feature_mock.Object);
    }
}
