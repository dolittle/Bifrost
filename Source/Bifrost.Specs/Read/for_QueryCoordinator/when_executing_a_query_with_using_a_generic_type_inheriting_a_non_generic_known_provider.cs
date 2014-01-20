using System;
using Bifrost.Read;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_with_using_a_generic_type_inheriting_a_non_generic_known_provider : given.a_query_coordinator_with_non_generic_known_provider
    {
        static QueryForGenericKnownType query;
        static PagingInfo paging;
        static GenericKnownType<object>  actual_query;
        static QueryProviderResult result;
        static ReadModelWithString[] items = new[] {
            new ReadModelWithString { Content = "Hello" },
            new ReadModelWithString { Content = "World" },
        };

        Establish context = () =>
        {
            query = new QueryForGenericKnownType();
            paging = new PagingInfo();

            actual_query = new GenericKnownType<object>();
            query.QueryToReturn = actual_query;

            result = new QueryProviderResult
            {
                Items = items
            };

            query_provider_mock.Setup(c => c.Execute(actual_query, paging)).Returns(result);
        };

        Because of = () => coordinator.Execute(query, paging);

        It should_forward_query_with_clause_to_provider = () => query_provider_mock.Verify(q => q.Execute(actual_query, paging), Moq.Times.Once());
        It should_filter_result = () => read_model_filters_mock.Verify(r => r.Filter(items), Times.Once());
    }
}
