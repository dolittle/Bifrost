#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
