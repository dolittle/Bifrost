using Machine.Specifications;
using Bifrost.Validation.MetaData;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator
{
    public class when_generating : given.a_validation_meta_data_generator_with_common_rules
    {
        static ObjectForValidationValidator validator;
        static ValidationMetaData meta_data;

        Establish context = () =>
        {
            validator = new ObjectForValidationValidator();
            
        };

        Because of = () => meta_data = generator.GenerateFrom(validator);

        It should_return_meta_data = () => meta_data.ShouldNotBeNull();
        It should_have_some_string_property = () => meta_data[ObjectForValidation.SomeStringName].ShouldNotBeEmpty();

        It should_have_required_for_string = () => meta_data[ObjectForValidation.SomeStringName]["required"].ShouldNotBeNull();
        It should_have_required_for_string_with_correct_message = () => meta_data[ObjectForValidation.SomeStringName]["required"].Message.ShouldEqual(ObjectForValidationValidator.NotEmptyErrorMessage);

        It should_have_email_for_string = () => meta_data[ObjectForValidation.SomeStringName]["email"].ShouldNotBeNull();
        It should_have_email_for_string_with_correct_message = () => meta_data[ObjectForValidation.SomeStringName]["email"].Message.ShouldEqual(ObjectForValidationValidator.EmailAddressErrorMessage);

        It should_have_less_than_for_int = () => meta_data[ObjectForValidation.SomeIntName]["lessThan"].ShouldNotBeNull();
        It should_have_less_than_for_int_with_correct_message = () => meta_data[ObjectForValidation.SomeIntName]["lessThan"].Message.ShouldEqual(ObjectForValidationValidator.LessThanErrorMessage);

        It should_have_greater_than_for_int = () => meta_data[ObjectForValidation.SomeIntName]["greaterThan"].ShouldNotBeNull();
        It should_have_greater_than_for_int_with_correct_message = () => meta_data[ObjectForValidation.SomeIntName]["greaterThan"].Message.ShouldEqual(ObjectForValidationValidator.GreaterThanErrorMessage);
    }
}
