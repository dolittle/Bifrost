/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a resource within the application
    /// </summary>
    public interface IApplicationResource
    {
        /// <summary>
        /// Gets the <see cref="ApplicationResourceName">name</see> of the resource
        /// </summary>
        ApplicationResourceName Name { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResourceType"/> of the resource
        /// </summary>
        IApplicationResourceType Type { get; }
    }
}
