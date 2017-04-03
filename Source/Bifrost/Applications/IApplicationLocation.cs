/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a location within an application
    /// </summary>
    public interface IApplicationLocation
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationName">application location name</see>
        /// </summary>
        IApplicationLocationName    Name { get; }
    }

    /// <summary>
    /// Defines a location within the application
    /// </summary>
    public interface IApplicationLocation<TName> : IApplicationLocation
        where TName: IApplicationLocationName
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationName">name</see> of the location
        /// </summary>
        new TName Name { get; }
    }
}
