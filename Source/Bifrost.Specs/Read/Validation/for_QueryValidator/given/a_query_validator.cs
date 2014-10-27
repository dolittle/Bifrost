using Bifrost.Read.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidator.given
{
    public class a_query_validator : all_dependencies
    {
        protected static QueryValidator query_validator;

        Establish context = () => query_validator = new QueryValidator(query_validation_descriptors_mock.Object);
    }
}
