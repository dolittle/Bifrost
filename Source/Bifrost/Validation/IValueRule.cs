/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines a rule for a value
    /// </summary>
    public interface IValueRule : IRule
    {
        /// <summary>
        /// Gets the property that represents the value
        /// </summary>
        PropertyInfo Property { get; }
    }
}
