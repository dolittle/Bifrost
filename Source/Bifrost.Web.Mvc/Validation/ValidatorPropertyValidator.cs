using System.Web.Mvc;
using Bifrost.Validation;
using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;

namespace Bifrost.Web.Mvc.Validation
{
    public class ValidatorPropertyValidator : FluentValidationPropertyValidator
    {
        public ValidatorPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
            ShouldValidate = false;

            var model = controllerContext.Controller.ViewData.Model;
            if (validator is PropertyValidatorWithDynamicState)
            {
                
                DynamicState = new DynamicState(model, ((PropertyValidatorWithDynamicState)validator).Properties);
            }
            else
            {
                DynamicState = new DynamicState(model);
            }
        }

        public dynamic DynamicState { get; private set; }
    }
}