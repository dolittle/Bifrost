using Bifrost.Read;
using Bifrost.Read.Validation;

namespace Bifrost.Specs.Commands.for_CommandValidators
{
    public class first_query_validator : IQueryValidator
    {
        public QueryValidationResult result_to_return;
        public bool validate_called;
        public IQuery query_passed_to_validate;

        public QueryValidationResult Validate(IQuery query)
        {
            validate_called = true;
            query_passed_to_validate = query;
            return result_to_return;
        }
    }
}
