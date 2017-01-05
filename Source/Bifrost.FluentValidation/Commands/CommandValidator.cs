/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Validation;

namespace Bifrost.FluentValidation.Commands
{
    /// <summary>
    /// Represents a <see cref="ICommandValidator">ICommandValidationService</see>
    /// </summary>
    [Singleton]
    public class CommandValidator : ICommandValidator
    {
        private readonly ICommandValidatorProvider _commandValidatorProvider;

        /// <summary>
        /// Initializes an instance of <see cref="CommandValidator"/> CommandValidationService
        /// </summary>
        /// <param name="commandValidatorProvider"></param>
        public CommandValidator(ICommandValidatorProvider commandValidatorProvider)
        {
            _commandValidatorProvider = commandValidatorProvider;
        }

#pragma warning disable 1591 // Xml Comments
        public CommandValidationResult Validate(ICommand command)
        {
            var result = new CommandValidationResult();
            var validationResults = ValidateInternal(command);
            result.ValidationResults = validationResults.Where(v => v.MemberNames.First() != ModelRule<object>.ModelRulePropertyName);
            result.CommandErrorMessages = validationResults.Where(v => v.MemberNames.First() == ModelRule<object>.ModelRulePropertyName).Select(v => v.ErrorMessage);
            return result;   
        }
#pragma warning restore 1591 // Xml Comments

        IEnumerable<ValidationResult> ValidateInternal(ICommand command)
        {
            var inputValidator = _commandValidatorProvider.GetInputValidatorFor(command);
            if (inputValidator != null)
            {
                var inputValidationErrors = inputValidator.ValidateFor(command);
                if (inputValidationErrors.Count() > 0)
                    return inputValidationErrors;
            }

            var businessValidator = _commandValidatorProvider.GetBusinessValidatorFor(command);
            if (businessValidator != null)
            {
                var businessValidationErrors = businessValidator.ValidateFor(command);
                return businessValidationErrors.Count() > 0 ? businessValidationErrors : new ValidationResult[0];
            }

            return new ValidationResult[0];
        }
    }
}