﻿using Bifrost.Exceptions;
using Bifrost.Execution;
using Bifrost.Read;
using Bifrost.Read.Validation;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.for_QueryCoordinator.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<IFetchingSecurityManager> fetching_security_manager_mock;
        protected static Mock<IReadModelFilters> read_model_filters_mock;
        protected static Mock<IQueryValidator> query_validator_mock;
        protected static Mock<IExceptionPublisher> exception_publisher_mock;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();
            fetching_security_manager_mock = new Mock<IFetchingSecurityManager>();
            read_model_filters_mock = new Mock<IReadModelFilters>();
            query_validator_mock = new Mock<IQueryValidator>();
            exception_publisher_mock = new Mock<IExceptionPublisher>();
        };
    }
}
