/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Validation.MetaData;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation.MetaData
{
    /// <summary>
    /// Represents the generator for generating a <see cref="Regex"/> rule from a <see cref="IRegularExpressionValidator"/>
    /// </summary>
    public class RegexGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(IRegularExpressionValidator), typeof(RegularExpressionValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            var rule = new Regex
            {
                Message = propertyValidator.GetErrorMessageFor(propertyName),
                Expression = ((IRegularExpressionValidator)propertyValidator).Expression
            };
            return rule;
        }
#pragma warning restore 1591 // Xml Comments

    }
}
