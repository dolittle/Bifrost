/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines a builder for building rules for value validation
    /// </summary>
    public class ValueValidationBuilder<TValue> : IValueValidationBuilder
    {
        List<IValueRule> _rules;

        /// <summary>
        /// Initializes an instance of <see cref="ValueValidationBuilder"/>
        /// </summary>
        /// <param name="property">Property that represents the value</param>
        public ValueValidationBuilder(PropertyInfo property)
        {
            _rules = new List<IValueRule>();
            Property = property;
        }

#pragma warning disable 1591 // Xml Comments
        public PropertyInfo Property { get; private set; }

        public IEnumerable<IValueRule> Rules { get { return _rules; } }

        public void AddRule(IValueRule rule)
        {
            _rules.Add(rule);
        }
#pragma warning restore 1591 // Xml Comments

    }
}
