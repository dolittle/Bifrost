using System;
using Bifrost.Read;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_with_a_known_provider_for_its_result_type : given.a_query_coordinator_with_known_provider
    {
        static QueryForKnownProvider query;
        static PagingInfo paging;
        
        
        static QueryType actual_query;

        Establish context = () =>
        {
            query = new QueryForKnownProvider();
            paging = new PagingInfo();

            actual_query = new QueryType();
            query.QueryToReturn = actual_query;
        };

        Because of = () => coordinator.Execute(query, paging);

        It should_forward_query_with_clause_to_provider = () => query_provider_mock.Verify(q => q.Execute(actual_query, paging), Moq.Times.Once());
    }
}
