/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Validation;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Defines a descriptor for describing the validation rules for a query
    /// </summary>
    public interface IQueryValidationDescriptor
    {
        /// <summary>
        /// Gets the argument rules
        /// </summary>
        IEnumerable<IValueRule> ArgumentRules { get; }
    }
}
