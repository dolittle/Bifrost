/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Tenancy
{
    /// <summary>
    /// Represents a <see cref="ITenant"/> in the system
    /// </summary>
    public class Tenant : ITenant
    {
#pragma warning disable 1591 // Xml Comments
        public WriteOnceExpandoObject Details { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
