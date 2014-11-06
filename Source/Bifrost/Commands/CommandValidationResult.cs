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
using Bifrost.Commands;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
