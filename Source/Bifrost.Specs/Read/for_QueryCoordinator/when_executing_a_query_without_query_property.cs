using System;
using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_without_query_property : given.a_query_coordinator
    {
        static IQuery query;
        static PagingInfo paging;
        static Exception exception;

        Establish   context = () => 
        {
            query = new QueryWithoutProperty();
            paging = new PagingInfo();
        };

        Because of = () => exception = Catch.Exception(() => coordinator.Execute(query, paging));

        It should_throw_the_no_query_property_exception = () => exception.ShouldBeOfType<NoQueryPropertyException>();
    }
}
