using Bifrost.Read.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationDescriptorFor
{
    public class when_describing_argument : given.an_empty_query_validation_descriptor
    {
        static QueryArgumentValidationBuilder<SomeQuery, int> builder;

        Because of = () => builder = descriptor.ForArgument(q => q.IntegerArgument);

        It should_hold_the_builder = () => descriptor.ArgumentsRuleBuilders.ShouldContainOnly(builder);
        It should_return_a_builder = () => builder.ShouldNotBeNull();
    }
}
