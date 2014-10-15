using System;
using Bifrost.Read;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator_with_provider_for_known_query_and_one_for_derived_type : all_dependencies
    {
        protected static Mock<IQueryProviderFor<QueryType>> query_provider_mock;
        protected static QueryCoordinator coordinator;
        protected static Type provider_type;
        protected static QueryProviderForDerivedType query_provider_for_derived_type;

        Establish context = () =>
        {
            query_provider_mock = new Mock<IQueryProviderFor<QueryType>>();
            provider_type = query_provider_mock.Object.GetType();

            query_provider_for_derived_type = new QueryProviderForDerivedType();

            type_discoverer_mock.Setup(t => t.FindMultiple(typeof(IQueryProviderFor<>))).Returns(new[] { provider_type, typeof(QueryProviderForDerivedType) });
            container_mock.Setup(c => c.Get(provider_type)).Returns(query_provider_mock.Object);
            container_mock.Setup(c => c.Get(typeof(QueryProviderForDerivedType))).Returns(query_provider_for_derived_type);

            fetching_security_manager_mock.Setup(f => f.Authorize(Moq.It.IsAny<IQuery>())).Returns(new AuthorizationResult());

            coordinator = new QueryCoordinator(
                type_discoverer_mock.Object,
                container_mock.Object,
                fetching_security_manager_mock.Object,
                query_validator_mock.Object,
                read_model_filters_mock.Object);
        };
    }
}
