using System;
using Bifrost.Read;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator_with_non_generic_known_provider : all_dependencies
    {
        protected static Mock<IQueryProviderFor<NonGenericKnownType>> query_provider_mock;
        protected static QueryCoordinator coordinator;
        protected static Type provider_type;

        Establish context = () =>
        {
            query_provider_mock = new Mock<IQueryProviderFor<NonGenericKnownType>>();
            provider_type = query_provider_mock.Object.GetType();

            type_discoverer_mock.Setup(t => t.FindMultiple(typeof(IQueryProviderFor<>))).Returns(new[] { provider_type });
            container_mock.Setup(c => c.Get(provider_type)).Returns(query_provider_mock.Object);

            coordinator = new QueryCoordinator(type_discoverer_mock.Object, container_mock.Object, read_model_filters_mock.Object);
        };
    }
}
