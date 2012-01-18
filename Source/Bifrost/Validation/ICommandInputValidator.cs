#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines a basic input level validator for a Command
    /// </summary>
    public interface ICommandInputValidator
    {
        /// <summary>
        /// Validates that a command has all the properties that are required for the command to succeed.
        /// </summary>
        /// <param name="command">The command to validate</param>
        /// <remarks>
        /// Only validates the type / presence / etc. of the properties.  It does not validate that they are correct from a business perspective.
        /// e.g. validates that we have a valid user id but not that this user id actually exists.
        /// </remarks>
        /// <returns>A collection of ValidationResults.  An empty collection indicates a valid command.</returns>
        IEnumerable<ValidationResult> ValidateInput(ICommand command);
    }
}