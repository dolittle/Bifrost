/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system that provide <see cref="IEventProcessor">event processors</see>
    /// </summary>
    public interface ICanProvideEventProcessors
    {
        /// <summary>
        /// Get all the <see cref="IEventProcessor">event processors</see>
        /// </summary>
        IEnumerable<IEventProcessor> EventProcessors { get; }
    }
}
