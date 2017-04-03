using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver.given
{
    public class an_identifier : all_dependencies
    {
        protected const string resource_name = "Implementation";
        protected const string resource_type_identifier = "MyResourceType";
        protected static ApplicationArea area = "MyArea";

        protected static Mock<IApplicationResourceIdentifier> identifier;
        protected static Mock<IApplicationResource> resource;
        protected static Mock<IApplicationResourceType> resource_type;

        Establish context = () =>
        {
            resource_type = new Mock<IApplicationResourceType>();
            resource_type.SetupGet(r => r.Identifier).Returns(resource_type_identifier);
            resource_type.SetupGet(r => r.Type).Returns(typeof(IInterface));
            resource_type.SetupGet(r => r.Area).Returns(area);

            resource = new Mock<IApplicationResource>();
            resource.SetupGet(r => r.Name).Returns(resource_name);
            resource.SetupGet(r => r.Type).Returns(resource_type.Object);
            

            identifier = new Mock<IApplicationResourceIdentifier>();
            identifier.SetupGet(i => i.Resource).Returns(resource.Object);

            application_resource_types.Setup(a => a.GetFor(resource_type_identifier)).Returns(resource_type.Object);
        };
    }
}
