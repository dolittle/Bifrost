using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationResult
{
    public class when_asking_for_success_on_result_with_one_broken_rule : given.a_query_validation_result_with_one_broken_rule
    {
        static bool success;

        Because of = () => success = result.Success;

        It should_be_considered_successful = () => success.ShouldBeFalse();
    }
}
