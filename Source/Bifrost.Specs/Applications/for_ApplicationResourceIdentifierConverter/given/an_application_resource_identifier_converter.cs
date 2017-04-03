using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifierConverter.given
{
    public class an_application_resource_identifier_converter : all_dependencies
    {
        protected const string application_name = "MyApplication";
        protected const string resource_type = "TheResourceType";

        protected static ApplicationResourceIdentifierConverter converter;

        protected static Mock<IApplicationResourceType> application_resource_type;

        Establish context = () =>
        {
            application.SetupGet(a => a.Name).Returns(application_name);

            application_resource_type = new Mock<IApplicationResourceType>();
            application_resource_type.SetupGet(a => a.Identifier).Returns(resource_type);

            application_resource_types.Setup(a => a.GetFor(resource_type)).Returns(application_resource_type.Object);

            converter = new ApplicationResourceIdentifierConverter(application.Object, application_resource_types.Object);
        };
    }
}
