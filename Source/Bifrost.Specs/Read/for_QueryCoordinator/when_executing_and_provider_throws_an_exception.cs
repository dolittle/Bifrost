using System;
using Bifrost.Read;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_and_provider_throws_an_exception : given.a_query_coordinator_with_known_provider
    {
        static QueryForKnownProvider query;
        static PagingInfo paging;
        static QueryType actual_query;
        static Exception exception_thrown;
        static QueryResult result;

        Establish context = () =>
        {
            query = new QueryForKnownProvider();
            paging = new PagingInfo();

            actual_query = new QueryType();
            query.QueryToReturn = actual_query;

            exception_thrown = new ArgumentException();

            query_provider_mock.Setup(q => q.Execute(actual_query, paging)).Throws(exception_thrown);
        };

        Because of = () => result = coordinator.Execute(query, paging);

        It should_set_the_exception_on_the_result = () => result.Exception.ShouldEqual(exception_thrown);

        It should_publish_the_exception = () => exception_publisher_mock.Verify(m => m.Publish(exception_thrown));
    }
}
