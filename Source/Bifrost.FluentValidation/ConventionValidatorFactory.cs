/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
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
            var validatorType = type.GetTypeInfo().Assembly.GetType(validatorTypeName);
            var validator = _container.Get(validatorType) as IValidator<T>;
            return validator;
        }

        public IValidator GetValidator(Type type)
        {
            var validatorTypeName = string.Format("{0}.{1}Validator", type.Namespace, type.Name);
            var validatorType = type.GetTypeInfo().Assembly.GetType(validatorTypeName);
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
