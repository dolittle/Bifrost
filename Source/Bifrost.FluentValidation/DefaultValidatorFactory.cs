/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.FluentValidation.Commands;
using FluentValidation;

namespace Bifrost.FluentValidation
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
                    !t.HasInterface<ICommandBusinessValidator>());
            foreach (var validatorType in validatorTypes)
            {
                var genericArguments = validatorType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments();
                if (genericArguments.Length == 1)
                {
                    var targetType = genericArguments[0];
                    _validatorsByType[targetType] = validatorType;
                }
            }
        }

    }
}
