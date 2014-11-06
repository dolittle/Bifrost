using System.Collections.Generic;
using Machine.Specifications;
using Bifrost.Validation.MetaData;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class when_generating_for_nested_properties : given.a_validation_meta_data_generator_with_common_rules
    {
        static NestedObjectForValidationValidator nestedValidator;
        static ObjectForValidationValidator objectValidator;
        static TypeMetaData meta_data;
        static string someObjectStringValue;
        static string someIntValue;
        static string firstLevelStringValue;

        Establish context = () =>
        {

            firstLevelStringValue = NestedObjectForValidation.FirstLevelStringName;
            someObjectStringValue = NestedObjectForValidation.SomeObjectName + "." + ObjectForValidation.SomeStringName;
            someIntValue = NestedObjectForValidation.SomeObjectName + "." + ObjectForValidation.SomeIntName;

            objectValidator = new ObjectForValidationValidator();
            nestedValidator = new NestedObjectForValidationValidator(objectValidator);

            container_mock.Setup(c => c.Get(typeof(ObjectForValidationValidator))).Returns(objectValidator);
            container_mock.Setup(c => c.Get(typeof(NestedObjectForValidationValidator))).Returns(nestedValidator);
        };

        Because of = () => meta_data = generator.GenerateFor(typeof(NestedObjectForValidation));

        It should_return_meta_data = () => meta_data.ShouldNotBeNull();

        It should_have_the_first_level_string_property = () => meta_data[firstLevelStringValue].ShouldNotBeEmpty();
        It should_have_the_some_object_property = () => meta_data[someObjectStringValue].ShouldNotBeEmpty();

        It should_have_required_for_first_level_string = () => meta_data[firstLevelStringValue]["required"].ShouldNotBeNull();
        It should_have_required_for_some_object_value = () => meta_data[someObjectStringValue]["required"].ShouldNotBeNull();
        It should_have_required_for_string_with_correct_message = () => meta_data[someObjectStringValue]["required"].Message.ShouldEqual(ObjectForValidationValidator.NotEmptyErrorMessage);

        It should_have_email_for_string = () => meta_data[someObjectStringValue]["email"].ShouldNotBeNull();
        It should_have_email_for_string_with_correct_message = () => meta_data[someObjectStringValue]["email"].Message.ShouldEqual(ObjectForValidationValidator.EmailAddressErrorMessage);

        It should_have_less_than_for_int = () => meta_data[someIntValue]["lessThan"].ShouldNotBeNull();
        It should_have_less_than_for_int_with_correct_message = () => meta_data[someIntValue]["lessThan"].Message.ShouldEqual(ObjectForValidationValidator.LessThanErrorMessage);

        It should_have_greater_than_for_int = () => meta_data[someIntValue]["greaterThan"].ShouldNotBeNull();
        It should_have_greater_than_for_int_with_correct_message = () => meta_data[someIntValue]["greaterThan"].Message.ShouldEqual(ObjectForValidationValidator.GreaterThanErrorMessage);
    }
}
