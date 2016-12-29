using Bifrost.Read;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_with_a_known_provider_for_a_derived_type_being_returned : given.a_query_coordinator_with_provider_for_known_query_and_one_for_derived_type
    {
        static QueryForKnownProvider query;
        static PagingInfo paging;
        static DerivedQueryType actual_query;
        static QueryProviderResult result;
        static ReadModelWithString[] items = new[] {
            new ReadModelWithString { Content = "Hello" },
            new ReadModelWithString { Content = "World" },
        };

        Establish context = () =>
        {

            query = new QueryForKnownProvider();
            paging = new PagingInfo();

            actual_query = new DerivedQueryType();
            query.QueryToReturn = actual_query;

            result = new QueryProviderResult {
                Items = items
            };

            query_provider_for_derived_type.result_to_return = result;
        };

        Because of = () => coordinator.Execute(query, paging);

        It should_forward_query_to_provider = () => query_provider_for_derived_type.query_passed_to_execute.ShouldEqual(actual_query);
        It should_forward_paging_info_to_provider = () => query_provider_for_derived_type.paging_info_passed_to_execute.ShouldEqual(paging);
        It should_filter_result = () => read_model_filters_mock.Verify(r => r.Filter(items),Times.Once());
    }
}
