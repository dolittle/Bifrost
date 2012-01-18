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
using FluentValidation;
using Microsoft.Practices.ServiceLocation;

namespace Bifrost.Validation
{
	/// <summary>
	/// Represents a <see cref="IValidatorFactory"/> that is based on conventions
	/// </summary>
    public class ConventionValidatorFactory : IValidatorFactory
    {
        readonly IServiceLocator _serviceLocator;

		/// <summary>
		/// Initializes an instance of <see cref="ConventionValidatorFactory"/>
		/// </summary>
		/// <param name="serviceLocator"><see cref="IServiceLocator"/> to use for getting instances of <see cref="IValidator">validators</see></param>
        public ConventionValidatorFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }
#pragma warning disable 1591 // Xml Comments
		public IValidator<T> GetValidator<T>()
        {
            var type = typeof(T);
            var validatorTypeName = string.Format("{0}Validator", type.Name);
            var validatorType = type.Assembly.GetType(validatorTypeName);

            var validator = _serviceLocator.GetInstance(validatorType) as IValidator<T>;
            return validator;
        }

        public IValidator GetValidator(Type type)
        {
            var validatorTypeName = string.Format("{0}.{1}Validator", type.Namespace, type.Name);
            var validatorType = type.Assembly.GetType(validatorTypeName);
            if (null != validatorType)
            {
                var validator = _serviceLocator.GetInstance(validatorType) as IValidator;
                return validator;
            }
            return null;
        }
    }
#pragma warning restore 1591 // Xml Comments
}
