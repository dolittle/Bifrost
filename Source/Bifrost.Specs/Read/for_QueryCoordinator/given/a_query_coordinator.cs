using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator : all_dependencies
    {
        protected static QueryCoordinator coordinator;

        Establish context = () =>
        {
            coordinator = new QueryCoordinator(type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
