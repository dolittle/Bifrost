using Bifrost.Applications;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifierConverter.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;
        protected static Mock<IApplicationResourceTypes> application_resource_types;

        Establish context = () =>
        {
            application = new Mock<IApplication>();
            application_resource_types = new Mock<IApplicationResourceTypes>();
        };
    }
}
