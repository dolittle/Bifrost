using Bifrost.Commands;
using Bifrost.Validation;
using Bifrost.Validation.MetaData;
using FluentValidation;
using Bifrost.Serialization;

namespace Bifrost.Web.Validation
{
    public class ValidationService
    {
        ICommandTypeManager _commandTypeManager;
        ICommandValidatorProvider _commandValidatorProvider;
        IValidationMetaDataGenerator _validationMetaDataGenerator;
        ISerializer _serializer;

        public ValidationService(
            ICommandTypeManager commandTypeManager,
            ICommandValidatorProvider commandValidatorProvider, 
            IValidationMetaDataGenerator validationMetaDataGenerator,
            ISerializer serializer)
        {
            _commandTypeManager = commandTypeManager;
            _commandValidatorProvider = commandValidatorProvider;
            _validationMetaDataGenerator = validationMetaDataGenerator;
            _serializer = serializer;
        }

        public ValidationMetaData GetForCommand(string name)
		{
            var commandType = _commandTypeManager.GetFromName(name);
            var inputValidator = _commandValidatorProvider.GetInputValidatorFor(commandType) as IValidator;
            if (inputValidator != null)
            {
                var metaData = _validationMetaDataGenerator.GenerateFrom(inputValidator);
                return metaData;
            }
            return new ValidationMetaData();
		}
    }
}

