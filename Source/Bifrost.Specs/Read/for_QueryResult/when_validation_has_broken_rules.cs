using Bifrost.Read;
using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_QueryResult
{
    public class when_validation_has_broken_rules
    {
        static QueryResult result;

        Because of = () => result = new QueryResult { 
            Validation = new QueryValidationResult(new[] { new BrokenRule() }),
            Items = new object[0]
        };

        It should_be_considered_unsuccessful = () => result.Success.ShouldBeFalse();
    }
}
