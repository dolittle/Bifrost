using Bifrost.Read;
using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_that_does_not_pass_validation : given.a_query_coordinator
    {
        static QueryForKnownProvider query;
        static PagingInfo paging;
        static QueryResult result;
        static QueryValidationResult validation_result;

        Establish context = () =>
        {
            query = new QueryForKnownProvider();
            paging = new PagingInfo();

            validation_result = new QueryValidationResult(new[] { new BrokenRule() });

            query_validator_mock.Setup(c => c.Validate(query)).Returns(validation_result);
        };

        Because of = () => result = coordinator.Execute(query, paging);

        It should_hold_the_validation_result = () => result.Validation.ShouldEqual(validation_result);
        It should_not_be_executing_query = () => query.QueryPropertyCalled.ShouldBeFalse();
        It should_have_hold_an_empty_items_array = () => result.Items.ShouldBeEmpty();
    }
}
