/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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