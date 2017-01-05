/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents a system for retrieving validation metadata
    /// </summary>
    public interface IValidationMetaData
    {
        /// <summary>
        /// Get metadata for a specific type
        /// </summary>
        /// <param name="typeForValidation">The <see cref="Type"/> that will be validated</param>
        /// <returns>The actual metadata</returns>
        TypeMetaData GetMetaDataFor(Type typeForValidation);
    }
}
