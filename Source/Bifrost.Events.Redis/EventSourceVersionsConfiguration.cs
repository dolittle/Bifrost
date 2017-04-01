/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events.Redis
{
    /// <summary>
    /// Represents the configuration for the Redis implementation of <see cref="IEventSequenceNumbers"/>
    /// </summary>
    public class EventSourceVersionsConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventSourceVersionsConfiguration"/>
        /// </summary>
        /// <param name="connectionStrings"></param>
        public EventSourceVersionsConfiguration(IEnumerable<string> connectionStrings)
        {
            ConnectionStrings = connectionStrings;
        }

        /// <summary>
        /// Gets the connection strings in form of <see cref="string"/>
        /// </summary>
        public IEnumerable<string> ConnectionStrings { get; }
    }
}