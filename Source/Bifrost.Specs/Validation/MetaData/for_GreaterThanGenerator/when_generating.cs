using Machine.Specifications;
using FluentValidation.Validators;
using Bifrost.Validation.MetaData;

namespace Bifrost.Specs.Validation.MetaData.for_GreaterThanGenerator
{
    public class when_generating
    {
        static GreaterThanValidator validator;
        static GreaterThanGenerator generator;
        static GreaterThan result;

        Establish context = () =>
        {
            validator = new GreaterThanValidator(5.7f);
            generator = new GreaterThanGenerator();
        };

        Because of = () => result = generator.GeneratorFrom(validator) as GreaterThan;

        It should_create_a_rule = () => result.ShouldNotBeNull();
        It should_pass_along_the_value = () => result.Value.ShouldEqual(validator.ValueToCompare);
    }
}
