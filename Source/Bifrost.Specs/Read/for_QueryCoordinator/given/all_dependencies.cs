using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();
        };
    }
}
