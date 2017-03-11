using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifier.given
{
    public class identifiers_with_different_applications
    {
        protected static ApplicationResourceIdentifier identifier_a;
        protected static ApplicationResourceIdentifier identifier_b;

        Establish context = () =>
        {
            var application_a = new Mock<IApplication>();
            application_a.SetupGet(a => a.Name).Returns("ApplicationA");

            var application_b = new Mock<IApplication>();
            application_b.SetupGet(a => a.Name).Returns("ApplicationB");

            var boundedContext = new BoundedContext("SomeBoundedContext");
            var module = new Module(boundedContext, "SomeModule");
            var feature = new Feature(module, "SomeFeature");

            var segments = new IApplicationLocation[] { boundedContext, module, feature };

            var application_resource_type = new Mock<IApplicationResourceType>();

            var resource = new ApplicationResource("SomeResource", application_resource_type.Object);

            identifier_a = new ApplicationResourceIdentifier(application_a.Object, segments, resource);
            identifier_b = new ApplicationResourceIdentifier(application_b.Object, segments, resource);
        };
    }
}
