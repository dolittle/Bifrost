using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Bifrost.Web.Mvc.Validation
{
    public class GreaterThanOrEqualPropertyValidator : FluentValidationPropertyValidator
    {
        public GreaterThanOrEqualPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
            ShouldValidate = false;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!ShouldGenerateClientSideRules()) yield break;

            var formatter = new MessageFormatter().AppendPropertyName(Rule.GetDisplayName());
            string message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());
            var clientRule = new ModelClientValidationRule
            {
                ValidationType = "greaterthanorequal",
                ErrorMessage = message,
            };
            clientRule.ValidationParameters.Add("valuetocompare", ((GreaterThanOrEqualValidator)Validator).ValueToCompare);

            yield return clientRule;
        }
    }
}
