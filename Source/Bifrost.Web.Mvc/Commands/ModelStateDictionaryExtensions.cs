#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Bifrost.Commands;
using System.Linq;

namespace Bifrost.Web.Mvc.Commands
{
    /// <summary>
    /// Extension methods for conviently working with <see cref="CommandResult"/> and <see cref="ModelStateDictionary"/>
    /// </summary>
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Add command result information onto a <see cref="ModelStateDictionary"/>
        /// </summary>
        /// <param name="modelStateDictionary"><see cref="ModelStateDictionary"/> to add to</param>
        /// <param name="commandResult"><see cref="CommandResult"/> to add from</param>
        /// <param name="exceptionErrorName">Optional parameter for specifying a error name for the any exception, if not specified the error name will be the name of the exception</param>
        public static void FromCommandResult(this ModelStateDictionary modelStateDictionary, CommandResult commandResult, string exceptionErrorName = null)
        {
            if (commandResult.Invalid)
            {
                modelStateDictionary.AddToModelErrors(commandResult.ValidationResults);

                if( commandResult.CommandValidationMessages != null ) 
                    modelStateDictionary.AddToModelErrors(commandResult.CommandValidationMessages);
            }
            else
            {
                if (exceptionErrorName == null)
                    exceptionErrorName = commandResult.Exception.GetType().Name;

                modelStateDictionary.AddModelError(exceptionErrorName, commandResult.Exception.Message);
            }
        }

        /// <summary>
        /// Add a string of messages to a <see cref="ModelStateDictionary"/>
        /// </summary>
        /// <param name="modelStateDictionary"><see cref="ModelStateDictionary"/> to add to</param>
        /// <param name="messages">Strings to add</param>
        public static void AddToModelErrors(this ModelStateDictionary modelStateDictionary, IEnumerable<string> messages)
        {
            foreach (var message in messages)
                modelStateDictionary.AddModelError(string.Empty, message);
        }

        /// <summary>
        /// Add <see cref="ValidationResult">ValidationResults</see> to <see cref="ModelStateDictionary"/>
        /// </summary>
        /// <param name="modelStateDictionary"><see cref="ModelStateDictionary"/> to add to</param>
        /// <param name="validationResults"><see cref="ValidationResult">ValidationResults</see> to add</param>
        /// <param name="prefix">Optional prefix to prepend to all member names</param>
        public static void AddToModelErrors(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResult> validationResults, string prefix = null)
        {
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    var key = string.IsNullOrWhiteSpace(prefix) ? memberName : string.Concat(prefix, ".", memberName);
                    modelStateDictionary.AddModelError(key, validationResult.ErrorMessage);
                }
            }
        }


        /// <summary>
        /// Turn a <see cref="ModelStateDictionary"/> into a <see cref="CommandResult"/>
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns></returns>
        public static CommandResult ToCommandResult(this ModelStateDictionary modelStateDictionary)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var ms in modelStateDictionary)
            {
                var key = ms.Key;
                if (ms.Value.Errors.Count > 0)
                {
                    validationResults.AddRange(ms.Value.Errors.Select(error => new ValidationResult(error.ErrorMessage, new[] { key })));
                }
            }

            return new CommandResult() { ValidationResults = validationResults };
        }
    }
}
