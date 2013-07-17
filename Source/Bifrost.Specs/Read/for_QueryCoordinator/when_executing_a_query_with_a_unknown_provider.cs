using System;
using Bifrost.Read;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_with_a_unknown_provider : given.a_query_coordinator
    {
        static QueryForUnknownProvider query;
        static PagingInfo paging;
        static QueryResult result;

        Establish context = () =>
        {
            query = new QueryForUnknownProvider();
            paging = new PagingInfo();
        };

        Because of = () => result = coordinator.Execute(query, paging);

        It should_throw_a_unknown_query_type_exception = () => result.Exception.ShouldBeOfType<UnknownQueryTypeException>();
    }
}
