using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryArgumentValidationResult.given
{
    public class a_query_argument_validation_result_with_one_broken_rule
    {
        protected static QueryArgumentValidationResult result;

        Establish context = () => result = new QueryArgumentValidationResult(new[] { new BrokenRule() });
    }
}
