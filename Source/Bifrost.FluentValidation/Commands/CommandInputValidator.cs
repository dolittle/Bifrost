#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
    /// <remarks>
    /// Commands inherting from this base class will be automatically registered.
    /// </remarks>
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