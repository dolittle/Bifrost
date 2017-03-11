using Bifrost.Applications;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanResolveApplicationResources>> resolvers;

        Establish context = () => resolvers = new Mock<IInstancesOf<ICanResolveApplicationResources>>();
    }
}
