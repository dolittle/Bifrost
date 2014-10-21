using Bifrost.FluentValidation.MetaData;
using Bifrost.Validation.MetaData;
using FluentValidation.Validators;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.MetaData.for_RegexGenerator
{
    public class when_generating
    {
        const string expression = "[abc]";
        static RegularExpressionValidator validator;
        static RegexGenerator generator;
        static Regex result;

        Establish context = () =>
        {
            validator = new RegularExpressionValidator(expression);
            generator = new RegexGenerator();
        };

        Because of = () => result = generator.GeneratorFrom("someProperty", validator) as Regex;

        It should_create_a_rule = () => result.ShouldNotBeNull();
        It should_pass_expression_along = () => result.Expression.ShouldEqual(expression);
    }
}
