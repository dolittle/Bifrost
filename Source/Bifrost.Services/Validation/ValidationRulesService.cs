using System;
using Bifrost.Commands;
using Bifrost.Validation;
using Bifrost.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System.Collections.Generic;
using System.Text;

namespace Bifrost.Services
{
    public delegate string TranslateValidator(IPropertyValidator propertyValidator);


    public class ValidationRulesService
    {
        ICommandTypeManager _commandTypeManager;
        ICommandValidatorProvider _commandValidatorProvider;

        public ValidationRulesService(
            ICommandTypeManager commandTypeManager,
            ICommandValidatorProvider commandValidatorProvider)
        {
            _commandTypeManager = commandTypeManager;
            _commandValidatorProvider = commandValidatorProvider;
        }

        Dictionary<Type, TranslateValidator> _validatorTranslators = new Dictionary<Type, TranslateValidator> {
            {typeof(INotEmptyValidator), TranslateToRequired },
            {typeof(INotNullValidator), TranslateToRequired },
            {typeof(IEmailValidator), TranslateToEMail },
            {typeof(LessThanValidator), TranslateToLessThan },
            {typeof(GreaterThanValidator), TranslateToGreaterThan }
        };

        static string TranslateToRequired(IPropertyValidator validator)
        {
            return "\"required\" : { \"message\": \"" + validator.ErrorMessageSource.GetString() +"\"}";
        }

        static string TranslateToEMail(IPropertyValidator validator)
        {
            return "\"email\" : { \"message\": \"" + validator.ErrorMessageSource.GetString() + "\"}";
        }

        static string TranslateToLessThan(IPropertyValidator validator)
        {
            return "\"lessThan\" : { \"message\": \"" + validator.ErrorMessageSource.GetString() + "\", \"value\":\""+((LessThanValidator)validator).ValueToCompare+"\"}";
        }

        static string TranslateToGreaterThan(IPropertyValidator validator)
        {
            return "\"greaterThan\" : { \"message\": \"" + validator.ErrorMessageSource.GetString() + "\", \"value\":\"" + ((GreaterThanValidator)validator).ValueToCompare + "\"}";
        }

        public string GetForCommand(string name)
		{
            var firstMember = true;
            var result = new StringBuilder();
            result.Append("{");
            //var name = "DoStuffCommand";
            var commandType = _commandTypeManager.GetFromName(name);
            var inputValidator = _commandValidatorProvider.GetInputValidatorFor(commandType) as IValidator;
            if (inputValidator != null)
            {
                var descriptor = inputValidator.CreateDescriptor();
                var members = descriptor.GetMembersWithValidators();
                foreach (var member in members)
                {
                    if (!firstMember)
                        result.Append(",");

                    firstMember = false;
                    result.Append(string.Format("\"{0}\" : {{", member.Key.ToCamelCase()));
                    var rules = descriptor.GetRulesForMember(member.Key);
                    foreach (var rule in rules)
                    {
                        var firstValidator = true;

                        foreach (var validator in rule.Validators)
                        {
                            if (validator is IPropertyValidator)
                            {
                                if (!firstValidator)
                                    result.Append(",");

                                firstValidator = false;

                                var validatorType = validator.GetType();
                                var types = new List<Type>();
                                types.Add(validatorType);
                                types.AddRange(validatorType.GetInterfaces());

                                foreach (var type in types)
                                {
                                    if (_validatorTranslators.ContainsKey(type))
                                    {
                                        result.Append(_validatorTranslators[type](validator as IPropertyValidator));
                                    }
                                }
                            }
                        }

                    }
                    result.Append("}");
                }
            }
            result.Append("}");
            return result.ToString();
		}
    }
}

