using Bifrost.Applications;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver.given
{
    public class all_dependencies
    {
        protected static Mock<IApplication> application;
        protected static Mock<IApplicationResourceTypes> application_resource_types;
        protected static Mock<IInstancesOf<ICanResolveApplicationResources>> resolvers;
        protected static Mock<ITypeDiscoverer> type_discoverer;
        protected static Mock<IApplicationStructure> application_structure;

        Establish context = () =>
        {
            application_structure = new Mock<IApplicationStructure>();
            application = new Mock<IApplication>();
            application.SetupGet(a => a.Structure).Returns(application_structure.Object);
            application_resource_types = new Mock<IApplicationResourceTypes>();
            resolvers = new Mock<IInstancesOf<ICanResolveApplicationResources>>();
            type_discoverer = new Mock<ITypeDiscoverer>();
        };
    }
}
