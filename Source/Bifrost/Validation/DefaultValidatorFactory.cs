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
using System;
using System.Linq;
using System.Collections.Generic;
using Bifrost.Extensions;
using Bifrost.Execution;
using FluentValidation;

#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents the default <see cref="IValidatorFactory"/> implementation used for validators
    /// </summary>
    public class DefaultValidatorFactory : IValidatorFactory
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        Dictionary<Type, Type> _validatorsByType;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultValidatorFactory"/>
        /// </summary>
        /// <param name="typeDiscoverer">A <see cref="ITypeDiscoverer"/> used for discovering validators</param>
        /// <param name="container">A <see cref="IContainer"/> to use for creating instances of the different validators</param>
        public DefaultValidatorFactory(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _container = container;
            _validatorsByType = new Dictionary<Type, Type>();
            _typeDiscoverer = typeDiscoverer;
            Populate();
        }

#pragma warning disable 1591 // Xml Comments
        public IValidator GetValidator(Type type)
        {
            if (!_validatorsByType.ContainsKey(type))
                return null;


            var instance = _container.Get(_validatorsByType[type]) as IValidator;
            return instance;
        }

        public IValidator<T> GetValidator<T>()
        {
            return GetValidator(typeof(T)) as IValidator<T>;
        }
#pragma warning restore 1591 // Xml Comments

        void Populate()
        {
            var validatorTypes = _typeDiscoverer.FindMultiple(typeof(IValidator)).Where(
                t =>
                    !t.HasInterface<ICommandInputValidator>() &&
                    !t.HasInterface<ICommandBusinessValidator>() &&
#if(NETFX_CORE)
                    !t.GetTypeInfo().BaseType.Name.Equals(typeof(ChapterValidator<>).Name)
#else
                    !t.BaseType.Name.Equals(typeof(ChapterValidator<>).Name)
#endif
                    );
            foreach (var validatorType in validatorTypes)
            {
#if(NETFX_CORE)
                var genericArguments = validatorType.GetTypeInfo().BaseType.GenericTypeArguments;
#else
                var genericArguments = validatorType.BaseType.GetGenericArguments();
#endif
                if (genericArguments.Length == 1)
                {
                    var targetType = genericArguments[0];
                    _validatorsByType[targetType] = validatorType;
                }
            }
        }

    }
}
