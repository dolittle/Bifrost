using Machine.Specifications;
using FluentValidation.Validators;
using Bifrost.Validation.MetaData;

namespace Bifrost.Specs.Validation.MetaData.for_EmailGenerator
{
    public class when_generating
    {
        static EmailValidator validator;
        static EmailGenerator generator;
        static Email result;

        Establish context = () =>
        {
            validator = new EmailValidator();
            generator = new EmailGenerator();
        };

        Because of = () => result = generator.GeneratorFrom(validator) as Email;

        It should_create_a_rule = () => result.ShouldNotBeNull();
    }
}
