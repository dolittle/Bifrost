using Bifrost.FluentValidation.Commands;
using Bifrost.FluentValidation.MetaData;
using Bifrost.Testing;
using Machine.Specifications;
using Moq;

namespace Bifrost.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator.given
{
    public class a_validation_meta_data_generator_with_common_rules
    {
        protected static ValidationMetaDataGenerator generator;
        protected static Mock<ICommandValidatorProvider> command_validator_provider_mock;

        Establish context = () =>
        {
            var generators = new ICanGenerateRule[]
            {
                new RequiredGenerator(),
                new EmailGenerator(),
                new LessThanGenerator(),
                new GreaterThanGenerator(),
                new NotNullGenerator(),
            }.AsInstancesOf();

            command_validator_provider_mock = new Mock<ICommandValidatorProvider>();

            generator = new ValidationMetaDataGenerator(generators, command_validator_provider_mock.Object);
        };
    }
}