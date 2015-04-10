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
using System;
using Bifrost.Execution;
using FluentValidation;

namespace Bifrost.FluentValidation
{
	/// <summary>
	/// Represents a <see cref="IValidatorFactory"/> that is based on conventions
	/// </summary>
    public class ConventionValidatorFactory : IValidatorFactory
    {
        readonly IContainer _container;

		/// <summary>
		/// Initializes an instance of <see cref="ConventionValidatorFactory"/>
		/// </summary>
		/// <param name="container"><see cref="IContainer"/> to use for getting instances of <see cref="IValidator">validators</see></param>
        public ConventionValidatorFactory(IContainer container)
        {
            _container = container;
        }
#pragma warning disable 1591 // Xml Comments
		public IValidator<T> GetValidator<T>()
        {
            var type = typeof(T);
            var validatorTypeName = string.Format("{0}Validator", type.Name);
            var validatorType = type.Assembly.GetType(validatorTypeName);
            var validator = _container.Get(validatorType) as IValidator<T>;
            return validator;
        }

        public IValidator GetValidator(Type type)
        {
            var validatorTypeName = string.Format("{0}.{1}Validator", type.Namespace, type.Name);
            var validatorType = type.Assembly.GetType(validatorTypeName);
            if (null != validatorType)
            {
                var validator = _container.Get(validatorType) as IValidator;
                return validator;
            }
            return null;
        }
    }
#pragma warning restore 1591 // Xml Comments
}
