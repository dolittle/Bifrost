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
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.FluentValidation.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="IValidatorFactory"/> for dealing with validation for commands
    /// </summary>
    public class CommandValidatorFactory : IValidatorFactory
    {
        readonly ICommandValidatorProvider _commandValidatorProvider;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandValidatorFactory"/>
        /// </summary>
        /// <param name="commandValidatorProvider"><see cref="ICommandValidatorProvider"/> to get validators from</param>
        public CommandValidatorFactory(ICommandValidatorProvider commandValidatorProvider)
        {
            _commandValidatorProvider = commandValidatorProvider;
        }
#pragma warning disable 1591 // Xml Comments
        public IValidator<T> GetValidator<T>()
        {
            var validator = _commandValidatorProvider.GetInputValidatorFor(typeof(T)) as IValidator<T>;
            return validator;
        }

        public IValidator GetValidator(Type type)
        {
            if (null != type)
            {
                var validator = _commandValidatorProvider.GetInputValidatorFor(type) as IValidator;
                return validator;
            }
            return null;
        }
#pragma warning restore 1591 // Xml Comments
    }
}