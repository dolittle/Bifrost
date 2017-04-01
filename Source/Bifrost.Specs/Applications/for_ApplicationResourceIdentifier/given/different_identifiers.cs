using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifier.given
{
    public class different_identifiers
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

            var segments = new IApplicationLocation[] { boundedContext, module, feature };

            var application_resource_type = new Mock<IApplicationResourceType>();

            var resource_a = new ApplicationResource("SomeResource", application_resource_type.Object);
            var resource_b = new ApplicationResource("SomeOtherResource", application_resource_type.Object);

            identifier_a = new ApplicationResourceIdentifier(application.Object, segments, resource_a);
            identifier_b = new ApplicationResourceIdentifier(application.Object, segments, resource_b);
        };
    }
}
