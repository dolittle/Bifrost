using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.Validation.for_QueryArgumentValidationResult.given
{
    public class a_query_argument_validation_result_with_one_broken_rule
    {
        protected static QueryArgumentValidationResult result;
        protected static Mock<IRule> rule_mock;
        protected static object instance;
        protected static Mock<IRuleContext> rule_context_mock;

        Establish context = () =>
        {
            rule_mock = new Mock<IRule>();
            instance = new object();
            rule_context_mock = new Mock<IRuleContext>();

            result = new QueryArgumentValidationResult(new[] { new BrokenRule(rule_mock.Object, instance, rule_context_mock.Object) });
        };
    }
}
