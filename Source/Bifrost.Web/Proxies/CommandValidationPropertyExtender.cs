using System;
using Bifrost.CodeGeneration.JavaScript;
using Bifrost.Validation;
using Bifrost.Validation.MetaData;
using FluentValidation;
using Newtonsoft.Json;

namespace Bifrost.Web.Proxies
{
    public class CommandValidationPropertyExtender : ICanExtendCommandProperty
    {
        ICommandValidatorProvider _commandValidatorProvider;
        IValidationMetaDataGenerator _validationMetaDataGenerator;

        public CommandValidationPropertyExtender(ICommandValidatorProvider commandValidatorProvider, IValidationMetaDataGenerator validationMetaDataGenerator)
        {
            _commandValidatorProvider = commandValidatorProvider;
            _validationMetaDataGenerator = validationMetaDataGenerator;
        }

        public void Extend(Type commandType, string propertyName, Observable observable)
        {
            var inputValidator = _commandValidatorProvider.GetInputValidatorFor(commandType) as IValidator;
            if (inputValidator != null)
            {
                var metaData = _validationMetaDataGenerator.GenerateFrom(inputValidator);
                if (metaData.Properties.ContainsKey(propertyName))
                {
                    var options = JsonConvert.SerializeObject(metaData.Properties[propertyName],
                        new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
                    observable.ExtendWith("validation", options);
                }
            }
        }
    }
}
