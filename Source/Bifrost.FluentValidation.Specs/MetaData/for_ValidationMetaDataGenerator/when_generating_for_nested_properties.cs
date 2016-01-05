using Bifrost.Validation.MetaData;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class when_generating_for_nested_properties : given.a_validation_meta_data_generator_with_common_rules
    {
        static TypeMetaData meta_data;
        static string someObjectStringValue;
        static string someIntValue;
        static string firstLevelStringValue;

        Establish context = () =>
        {
            firstLevelStringValue = NestedCommandForValidation.FirstLevelStringName;
            someObjectStringValue = NestedCommandForValidation.SomeObjectName + "." + CommandForValidation.SomeStringName;
            someIntValue = NestedCommandForValidation.SomeObjectName + "." + CommandForValidation.SomeIntName;

            command_validator_provider_mock
                .Setup(m => m.GetInputValidatorFor(typeof(NestedCommandForValidation)))
                .Returns(new NestedCommandForValidationValidator(new CommandForValidationValidator()));
        };

        Because of = () => meta_data = generator.GenerateFor(typeof(NestedCommandForValidation));

        It should_return_meta_data = () => meta_data.ShouldNotBeNull();

        It should_have_the_first_level_string_property = () => meta_data[firstLevelStringValue].ShouldNotBeEmpty();
        It should_have_the_some_object_property = () => meta_data[someObjectStringValue].ShouldNotBeEmpty();

        It should_have_required_for_first_level_string = () => meta_data[firstLevelStringValue]["required"].ShouldNotBeNull();
        It should_have_required_for_some_object_value = () => meta_data[someObjectStringValue]["required"].ShouldNotBeNull();
        It should_have_required_for_string_with_correct_message = () => meta_data[someObjectStringValue]["required"].Message.ShouldEqual(CommandForValidationValidator.NotEmptyErrorMessage);

        It should_have_email_for_string = () => meta_data[someObjectStringValue]["email"].ShouldNotBeNull();
        It should_have_email_for_string_with_correct_message = () => meta_data[someObjectStringValue]["email"].Message.ShouldEqual(CommandForValidationValidator.EmailAddressErrorMessage);

        It should_have_less_than_for_int = () => meta_data[someIntValue]["lessThan"].ShouldNotBeNull();
        It should_have_less_than_for_int_with_correct_message = () => meta_data[someIntValue]["lessThan"].Message.ShouldEqual(CommandForValidationValidator.LessThanErrorMessage);

        It should_have_greater_than_for_int = () => meta_data[someIntValue]["greaterThan"].ShouldNotBeNull();
        It should_have_greater_than_for_int_with_correct_message = () => meta_data[someIntValue]["greaterThan"].Message.ShouldEqual(CommandForValidationValidator.GreaterThanErrorMessage);
    }
}
