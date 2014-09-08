using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryArgumentValidationResult.given
{
    public class a_query_argument_validation_result_with_null_passed_as_broken_rules
    {
        protected static QueryArgumentValidationResult result;

        Establish context = () => result = new QueryArgumentValidationResult(null);
    }
}
