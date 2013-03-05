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
using System;
using FluentValidation;

namespace Bifrost.Validation
{
#pragma warning disable 1591 // Xml Comments
    public class CommandValidatorFactory : IValidatorFactory
    {
        readonly ICommandValidatorProvider _commandValidatorProvider;

        public CommandValidatorFactory(ICommandValidatorProvider commandValidatorProvider)
        {
            _commandValidatorProvider = commandValidatorProvider;
        }

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
    }
#pragma warning restore 1591 // Xml Comments
}