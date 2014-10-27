using System.Linq;
using Bifrost.Read.Validation;
using Bifrost.Rules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.Validation.for_QueryValidator
{
    public class when_validating_query_with_three_rules_and_two_are_broken : given.a_query_validator
    {
        static SomeQuery query;
        static QueryValidationResult result;
        static Mock<IQueryValidationDescriptor> descriptor_mock;
        static Mock<IRule> first_rule_broken;
        static Mock<IRule> second_rule_not_broken;
        static Mock<IRule> third_rule_broken;

        static BrokenRuleReason first_broken_rule_first_reason;
        static BrokenRuleReason first_broken_rule_second_reason;
        static BrokenRuleReason third_broken_rule_reason;

        Establish context = () =>
        {
            query = new SomeQuery();

            first_rule_broken = new Mock<IRule>();
            second_rule_not_broken = new Mock<IRule>();
            third_rule_broken = new Mock<IRule>();

            first_broken_rule_first_reason = BrokenRuleReason.Create("1-1");
            first_broken_rule_second_reason = BrokenRuleReason.Create("1-2");
            third_broken_rule_reason = BrokenRuleReason.Create("2");

            first_rule_broken.Setup(f => f.Evaluate(Moq.It.IsAny<IRuleContext>(), Moq.It.IsAny<object>())).Callback((IRuleContext context, object instance) =>
            {
                context.Fail(first_rule_broken.Object, instance, first_broken_rule_first_reason);
                context.Fail(first_rule_broken.Object, instance, first_broken_rule_second_reason);
            });

            third_rule_broken.Setup(f => f.Evaluate(Moq.It.IsAny<IRuleContext>(), Moq.It.IsAny<object>())).Callback((IRuleContext context, object instance) =>
            {
                context.Fail(third_rule_broken.Object, instance, third_broken_rule_reason);
            });

            descriptor_mock = new Mock<IQueryValidationDescriptor>();
            descriptor_mock.SetupGet(d => d.ArgumentRules).Returns(new IRule[] {
                first_rule_broken.Object,
                second_rule_not_broken.Object,
                third_rule_broken.Object
            });

            query_validation_descriptors_mock.Setup(q => q.HasDescriptorFor<SomeQuery>()).Returns(true);
            query_validation_descriptors_mock.Setup(q => q.GetDescriptorFor<SomeQuery>()).Returns(descriptor_mock.Object);
        };

        Because of = () => result = query_validator.Validate(query);

        It should_be_unsuccessul = () => result.Success.ShouldBeFalse();
        It should_have_two_broken_rules = () => result.BrokenRules.Count().ShouldEqual(2);
        It should_hold_a_broken_rule_with_first_broken_rule_reasons = () => result.BrokenRules.First().Reasons.ShouldContainOnly(first_broken_rule_first_reason, first_broken_rule_second_reason);
        It should_hold_a_broken_rule_with_second_broken_rule_reasons = () => result.BrokenRules.ToArray()[1].Reasons.ShouldContainOnly(first_broken_rule_second_reason);
    }
}
