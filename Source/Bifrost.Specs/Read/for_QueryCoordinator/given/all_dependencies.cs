using Bifrost.Execution;
using Bifrost.Read;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<IReadModelFilters> read_model_filters_mock;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();
            read_model_filters_mock = new Mock<IReadModelFilters>();
        };
    }
}
