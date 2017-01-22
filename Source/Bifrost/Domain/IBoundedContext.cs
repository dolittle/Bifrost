/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Domain
{
    /// <summary>
    /// Defines a the concept of a bounded context from Domain Driven Design
    /// </summary>
    public interface IBoundedContext
    {
        /// <summary>
        /// Gets the modules within the <see cref="BoundedContext"/>
        /// </summary>
        IEnumerable<IModule> Modules { get; }

        /// <summary>
        /// Gets the name of the <see cref="BoundedContext"/>
        /// </summary>
        BoundedContextName Name { get; }

        /// <summary>
        /// Adds a <see cref="Module"/> to the <see cref="BoundedContext"/>
        /// </summary>
        /// <param name="module"><see cref="Module"/> to add</param>
        void AddModule(IModule module);
    }
}
