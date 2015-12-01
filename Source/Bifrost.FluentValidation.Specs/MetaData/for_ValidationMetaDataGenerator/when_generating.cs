using Bifrost.Validation.MetaData;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class when_generating : given.a_validation_meta_data_generator_with_common_rules
    {
        static TypeMetaData meta_data;

        Establish context = () =>
        {
            command_validator_provider_mock
                .Setup(m => m.GetInputValidatorFor(typeof (CommandForValidation)))
                .Returns(new CommandForValidationValidator());
        };

        Because of = () => meta_data = generator.GenerateFor(typeof(CommandForValidation));

        It should_return_meta_data = () => meta_data.ShouldNotBeNull();
        It should_have_some_string_property = () => meta_data[CommandForValidation.SomeStringName].ShouldNotBeEmpty();

        It should_have_required_for_string = () => meta_data[CommandForValidation.SomeStringName]["required"].ShouldNotBeNull();
        It should_have_required_for_string_with_correct_message = () => meta_data[CommandForValidation.SomeStringName]["required"].Message.ShouldEqual(CommandForValidationValidator.NotEmptyErrorMessage);

        It should_have_email_for_string = () => meta_data[CommandForValidation.SomeStringName]["email"].ShouldNotBeNull();
        It should_have_email_for_string_with_correct_message = () => meta_data[CommandForValidation.SomeStringName]["email"].Message.ShouldEqual(CommandForValidationValidator.EmailAddressErrorMessage);

        It should_have_less_than_for_int = () => meta_data[CommandForValidation.SomeIntName]["lessThan"].ShouldNotBeNull();
        It should_have_less_than_for_int_with_correct_message = () => meta_data[CommandForValidation.SomeIntName]["lessThan"].Message.ShouldEqual(CommandForValidationValidator.LessThanErrorMessage);

        It should_have_greater_than_for_int = () => meta_data[CommandForValidation.SomeIntName]["greaterThan"].ShouldNotBeNull();
        It should_have_greater_than_for_int_with_correct_message = () => meta_data[CommandForValidation.SomeIntName]["greaterThan"].Message.ShouldEqual(CommandForValidationValidator.GreaterThanErrorMessage);
    }
}
