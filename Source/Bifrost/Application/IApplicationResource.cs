/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Application
{
    /// <summary>
    /// Defines a resource within the application
    /// </summary>
    public interface IApplicationResource
    {
        /// <summary>
        /// Gets the <see cref="IApplicationResourceName">name</see> of the resource
        /// </summary>
        IApplicationResourceName Name { get; }
    }
}
