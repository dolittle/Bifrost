using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifier.given
{
    public class identifiers_with_different_locations
    {
        protected static ApplicationResourceIdentifier identifier_a;
        protected static ApplicationResourceIdentifier identifier_b;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("SomeApplication");

            var boundedContext = new BoundedContext("SomeBoundedContext");
            var module = new Module(boundedContext, "SomeModule");
            var feature = new Feature(module, "SomeFeature");
            var subFeature = new SubFeature(feature, "SomeSubFeature");

            var segments_a = new IApplicationLocation[] { boundedContext, module, feature };
            var segments_b = new IApplicationLocation[] { boundedContext, module, feature, subFeature };

            var resource = new ApplicationResource("SomeResource");

            identifier_a = new ApplicationResourceIdentifier(application.Object, segments_a, resource);
            identifier_b = new ApplicationResourceIdentifier(application.Object, segments_b, resource);
        };
    }
}
