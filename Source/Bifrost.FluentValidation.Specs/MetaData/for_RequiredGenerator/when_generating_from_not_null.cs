using Bifrost.FluentValidation.MetaData;
using Bifrost.Validation.MetaData;
using FluentValidation.Validators;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.MetaData.for_RequiredGenerator
{
    public class when_generating_from_not_null
    {
        static NotNullValidator validator;
        static RequiredGenerator generator;
        static Required result;

        Establish context = () =>
        {
            validator = new NotNullValidator();
            generator = new RequiredGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty",validator) as Required;

        It should_create_a_rule = () => result.ShouldNotBeNull();
    }
}
