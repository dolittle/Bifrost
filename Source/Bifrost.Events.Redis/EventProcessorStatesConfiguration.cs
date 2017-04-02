/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events.Redis
{
    /// <summary>
    /// Represents the configuration for the Redis implementation of <see cref="IEventProcessorStates"/>
    /// </summary>
    public class EventProcessorStatesConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessorStatesConfiguration"/>
        /// </summary>
        /// <param name="connectionStrings"></param>
        public EventProcessorStatesConfiguration(IEnumerable<string> connectionStrings)
        {
            ConnectionStrings = connectionStrings;
        }

        /// <summary>
        /// Gets the connection strings in form of <see cref="string"/>
        /// </summary>
        public IEnumerable<string> ConnectionStrings { get; }
    }
}