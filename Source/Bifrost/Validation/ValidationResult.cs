/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a container for the results of a validation request
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValidationResult"/>
        /// </summary>
        /// <param name="errorMessage">Error message for the validation result</param>
        public ValidationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationResult"/>
        /// </summary>
        /// <param name="errorMessage">Error message for the validation result</param>
        /// <param name="memberNames">The members for the validation result</param>
        public ValidationResult(string errorMessage, IEnumerable<string> memberNames)
        {
            ErrorMessage = errorMessage;
            MemberNames = memberNames;
        }


        /// <summary>
        /// Gets the error message for the validation.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets the collection of member names that indicate which fields have validation
        /// </summary>
        /// <returns>The collection of member names that indicate which fields have validation errors.</returns>
        public IEnumerable<string> MemberNames { get; private set; }
    }
}