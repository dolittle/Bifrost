/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a the concept of a bounded context from Domain Driven Design
    /// </summary>
    public interface IBoundedContext : IApplicationLocation<BoundedContextName>, ICanHaveApplicationLocationsOfType<IModule>
    {
        /// <summary>
        /// Adds a <see cref="Module"/> to the <see cref="BoundedContext"/>
        /// </summary>
        /// <param name="module"><see cref="Module"/> to add</param>
        void AddModule(IModule module);
    }
}