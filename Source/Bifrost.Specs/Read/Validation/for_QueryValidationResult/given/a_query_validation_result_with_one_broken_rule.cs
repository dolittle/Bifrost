using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationResult.given
{
    public class a_query_validation_result_with_one_broken_rule
    {
        protected static QueryValidationResult result;

        Establish context = () => result = new QueryValidationResult(new [] {new BrokenRule()});
    }
}
