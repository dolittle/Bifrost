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

        Establish context = () =>
        {
            application = new Mock<IApplication>();
            application_resource_types = new Mock<IApplicationResourceTypes>();
            resolvers = new Mock<IInstancesOf<ICanResolveApplicationResources>>();
            type_discoverer = new Mock<ITypeDiscoverer>();
        };
    }
}
