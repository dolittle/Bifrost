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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;

namespace Bifrost.Validation
{
    /// <summary>
    /// Base class to inherit from for basic business-rule validation of a command.
    /// </summary>
    /// <remarks>
    /// Commands inherting from this base class will be automatically registered.
    /// </remarks>
    /// <typeparam name="T">Concrete type of the Command to validate</typeparam>
    public abstract class CommandBusinessValidator<T> : ICanValidate<T>, ICommandBusinessValidator where T : class, ICommand
    {
#pragma warning disable 1591 // Xml Comments
        public virtual IEnumerable<ValidationResult> Validate(ICommand command)
        {
            return Validate(command as T);
        }
#pragma warning restore 1591 // Xml Comments

        /// <summary>
        /// Abstract Validate method, utilising the concrete type of the command, to be implemented by each Commmand-specific implementation
        /// </summary>
        /// <param name="instance">Concrete instance of the command</param>
        /// <returns>A collection of failed validations</returns>
        public abstract IEnumerable<ValidationResult> Validate(T instance);
    }
}