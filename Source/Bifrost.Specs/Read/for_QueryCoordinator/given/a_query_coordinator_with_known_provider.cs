﻿using System;
using Bifrost.Read;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator_with_known_provider : a_query_coordinator
    {
        protected static Mock<IQueryProviderFor<QueryType>> query_provider_mock;
        protected static Type provider_type;

        Establish context = () =>
        {
            query_provider_mock = new Mock<IQueryProviderFor<QueryType>>();
            provider_type = query_provider_mock.Object.GetType();

            type_discoverer_mock.Setup(t => t.FindMultiple(typeof(IQueryProviderFor<>))).Returns(new[] { provider_type });
            container_mock.Setup(c => c.Get(provider_type)).Returns(query_provider_mock.Object);

            fetching_security_manager_mock.Setup(f => f.Authorize(Moq.It.IsAny<IQuery>())).Returns(new AuthorizationResult());

            coordinator = new QueryCoordinator(
                type_discoverer_mock.Object,
                container_mock.Object,
                fetching_security_manager_mock.Object,
                query_validator_mock.Object,
                read_model_filters_mock.Object,
                exception_publisher_mock.Object);
        };
    }
}
