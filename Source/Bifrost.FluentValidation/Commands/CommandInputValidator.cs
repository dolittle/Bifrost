/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.FluentValidation.Commands
{
    /// <summary>
    /// Base class to inherit from for basic input validation of a command.
    /// </summary>
    /// <typeparam name="T">Concrete type of the Command to validate</typeparam>
    public abstract class CommandInputValidator<T> : InputValidator<T>, ICanValidate<T>, ICommandInputValidator where T : class, ICommand
    {
#pragma warning disable 1591 // Xml Comments
        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command as T);
            return BuildValidationResults(result);
        }

        public virtual IEnumerable<ValidationResult> ValidateFor(T command, string ruleSet)
        {
            var result = (this as IValidator<T>).Validate(command as T, ruleSet: ruleSet);
            return BuildValidationResults(result);
        }

        private static IEnumerable<ValidationResult> BuildValidationResults(global::FluentValidation.Results.ValidationResult result)
        {
            return result.Errors.Select(error =>
            {
                // TODO: Due to a problem with property names being wrong when a concepts input validator is involved, we need to do this. See #494 for more details on what needs to be done!
                var propertyName = error.PropertyName;
                if (propertyName.EndsWith(".")) propertyName = propertyName.Substring(0, propertyName.Length - 1);

                var validationResult = new ValidationResult(error.ErrorMessage, new[] { propertyName });
                return validationResult;
            });
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }

#pragma warning restore 1591 // Xml Comments
    }
}