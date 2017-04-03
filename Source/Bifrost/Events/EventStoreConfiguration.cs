/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the configuration related to <see cref="IEventStore"/>
    /// </summary>
    public class EventStoreConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventStoreConfiguration"/>
        /// </summary>
        public EventStoreConfiguration()
        {
            EventStore = typeof(NullEventStore);
        }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventStore"/> to use for handling events
        /// </summary>
        public Type EventStore { get; set; }
    }
}