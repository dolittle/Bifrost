/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Tenancy
{
    /// <summary>
    /// Defines a tenant in the system
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Gets the details for the tenant
        /// </summary>
        WriteOnceExpandoObject Details { get; }
    }
}
