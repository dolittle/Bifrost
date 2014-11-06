using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.Validation.for_QueryValidator.given
{
    public class all_dependencies
    {
        protected static Mock<IQueryValidationDescriptors> query_validation_descriptors_mock;
        protected static Mock<IRuleContexts> rule_contexts_mock;
        protected static Mock<IRuleContext> rule_context_mock;

        Establish context = () =>
        {
            query_validation_descriptors_mock = new Mock<IQueryValidationDescriptors>();
            rule_context_mock = new Mock<IRuleContext>();

            rule_contexts_mock = new Mock<IRuleContexts>();
            rule_contexts_mock.Setup(r => r.GetFor(Moq.It.IsAny<object>())).Returns(rule_context_mock.Object);
        };
    }
}
