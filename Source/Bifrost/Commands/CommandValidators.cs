/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Validation;

namespace Bifrost.Commands
{

    /// <summary>
    /// Represents an implementation of <see cref="ICommandValidators"/> 
    /// </summary>
    public class CommandValidators : ICommandValidators
    {
        IInstancesOf<ICommandValidator> _validators;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandValidators"/>
        /// </summary>
        /// <param name="validators">Instances of validators to use</param>
        public CommandValidators(IInstancesOf<ICommandValidator> validators)
        {
            _validators = validators;
        }

#pragma warning disable 1591 // Xml Comments
        public CommandValidationResult Validate(ICommand command)
        {
            var errorMessages = new List<string>();
            var validationResults = new List<ValidationResult>();

            foreach (var validator in _validators)
            {
                var validatorResult = validator.Validate(command);
                errorMessages.AddRange(validatorResult.CommandErrorMessages);
                validationResults.AddRange(validatorResult.ValidationResults);
            }
            var result = new CommandValidationResult
            {
                CommandErrorMessages = errorMessages,
                ValidationResults = validationResults
            };
            return result;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
