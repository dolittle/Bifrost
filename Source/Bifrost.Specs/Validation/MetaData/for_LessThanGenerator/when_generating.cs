using Machine.Specifications;
using FluentValidation.Validators;
using Bifrost.Validation.MetaData;

namespace Bifrost.Specs.Validation.MetaData.for_LessThanGenerator
{
    public class when_generating
    {
        static LessThanValidator validator;
        static LessThanGenerator generator;
        static LessThan result;

        Establish context = () =>
        {
            validator = new LessThanValidator(5.7f);
            generator = new LessThanGenerator();
        };

        Because of = () => result = generator.GeneratorFrom(validator) as LessThan;

        It should_create_a_rule = () => result.ShouldNotBeNull();
        It should_pass_along_the_value = () => result.Value.ShouldEqual(validator.ValueToCompare);
    }
}
