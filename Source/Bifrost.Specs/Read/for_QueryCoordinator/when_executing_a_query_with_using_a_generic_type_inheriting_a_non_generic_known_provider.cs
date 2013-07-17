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

        Establish context = () =>
        {
            query = new QueryForGenericKnownType();
            paging = new PagingInfo();

            actual_query = new GenericKnownType<object>();
            query.QueryToReturn = actual_query;
        };

        Because of = () => coordinator.Execute(query, paging);

        It should_forward_query_with_clause_to_provider = () => query_provider_mock.Verify(q => q.Execute(actual_query, paging), Moq.Times.Once());
    }
}
