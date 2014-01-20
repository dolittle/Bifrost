using Bifrost.Read;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator : all_dependencies
    {
        protected static QueryCoordinator coordinator;

        Establish context = () =>
        {
            fetching_security_manager_mock.Setup(f => f.Authorize(Moq.It.IsAny<IQuery>())).Returns(new AuthorizationResult());   
            coordinator = new QueryCoordinator(type_discoverer_mock.Object, container_mock.Object, fetching_security_manager_mock.Object, read_model_filters_mock.Object);
        };
    }
}
