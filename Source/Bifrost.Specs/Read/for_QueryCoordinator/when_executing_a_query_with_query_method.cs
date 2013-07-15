using System;
using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_with_query_method : given.a_query_coordinator
    {
        static IQuery query;
        static Clauses clauses;
        static Exception exception;

        Establish   context = () => 
        {
            query = new QueryWithQueryMethod();
            clauses = new Clauses();
        };

        Because of = () => exception = Catch.Exception(() => coordinator.Execute(query, clauses));

        It should_throw_the_no_query_property_exception = () => exception.ShouldBeOfType<NoQueryPropertyException>();
    }
}
