/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Security.Principal;
using Bifrost.Tenancy;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines the execution context in which things are within
    /// For instance, any commands coming into the system will be in the context of an execution context
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Gets the <see cref="IPrincipal"/> for the execution context
        /// </summary>
        IPrincipal Principal { get; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the execution context
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the string identifying the currently executing system
        /// </summary>
        string System { get; }

        /// <summary>
        /// Gets the tenant for the current execution context
        /// </summary>
        ITenant Tenant { get; }

        /// <summary>
        /// Gets the details for the execution context
        /// </summary>
        /// <remarks>
        /// This object is a write once object, meaning that you can't write to it at will.
        /// It can be populated by implementing a <see cref="ICanPopulateExecutionContextDetails"/>
        /// </remarks>
        dynamic Details { get; }
    }
}
