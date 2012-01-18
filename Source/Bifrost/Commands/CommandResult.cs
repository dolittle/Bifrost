#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents the result from the <see cref="ICommandCoordinator">CommandCoordinator</see>
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Gets or sets the ValidationResults generated during handling of a command
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; set; }

        /// <summary>
        /// Gets or sets the exception, if any, that occured during a handle
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets the success state of the result
        ///
        /// If there are invalid validationresult, this is false.
        /// If an exception occured, this is false.
        /// Otherwise, its true
        /// </summary>
        public bool Success
        {
            get { return null == Exception && !Invalid; }
        }

        /// <summary>
        /// Gets the validation state of the result
        ///
        /// If there are any validationresults this returns false, true if not
        /// </summary>
        public bool Invalid
        {
            get { return ValidationResults != null && ValidationResults.Count() > 0; }
        }

        /// <summary>
        /// Merges this instance of a CommandResult with another
        /// </summary>
        /// <param name="commandResultToMerge">The <see cref="CommandResult"/> to merge with the current instance</param>
        public void MergeWith(CommandResult commandResultToMerge)
        {
            if (Exception == null)
                Exception = commandResultToMerge.Exception;

            if (commandResultToMerge.ValidationResults == null) 
                return;

            if (ValidationResults == null)
            {
                ValidationResults = commandResultToMerge.ValidationResults;
                return;
            }

            var validationResults = ValidationResults.ToList();
            validationResults.AddRange(commandResultToMerge.ValidationResults);
            ValidationResults = validationResults.ToArray();
        }
    }
}