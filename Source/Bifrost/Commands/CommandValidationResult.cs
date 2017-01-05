/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Validation;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents the result of validation for a <see cref="ICommand"/>
    /// </summary>
    public class CommandValidationResult
    {
        /// <summary>
        /// Initializes an instance of <see cref="CommandValidationResult"/>
        /// </summary>
        public CommandValidationResult()
        {
            CommandErrorMessages = new string[0];
            ValidationResults = new ValidationResult[0];
        }

        /// <summary>
        /// Gets or sets the error messages related to a command, typically as a result of a failing ModelRule used from the <see cref="Validator"/>
        /// </summary>
        public IEnumerable<string> CommandErrorMessages { get; set; }

        /// <summary>
        /// Gets or sets the validation results from any validator
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}
