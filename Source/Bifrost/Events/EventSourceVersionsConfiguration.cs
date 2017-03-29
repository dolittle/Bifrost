/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the configuration for <see cref="IEventSourceVersions"/>
    /// </summary>
    public class EventSourceVersionsConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventSourceVersionsConfiguration"/>
        /// </summary>
        public EventSourceVersionsConfiguration()
        {
            EventSourceVersions = typeof(NullEventSourceVersions);
        }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventSourceVersions"/> to use
        /// </summary>
        public Type EventSourceVersions { get; set; }
    }
}
