/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the configuration related to <see cref="IEventProcessorStates"/>
    /// </summary>
    public class EventProcessorStatesConfiguration
    {
        /// <summary>
        /// Initializes a new instance og <see cref="EventProcessorStatesConfiguration"/>
        /// </summary>
        public EventProcessorStatesConfiguration()
        {
            EventProcessorStates = typeof(NullEventProcessorStates);
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> of a <see cref="IEventProcessorStates"/>
        /// </summary>
        public Type EventProcessorStates { get; set; }
    }
}