/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the configuration for <see cref="IEventSequenceNumbers"/>
    /// </summary>
    public class EventSequenceNumbersConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventSequenceNumbersConfiguration"/>
        /// </summary>
        public EventSequenceNumbersConfiguration()
        {
            EventSequenceNumbers = typeof(NullEventSequenceNumbers);
        }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventSequenceNumbers"/> to use for generating <see cref="EventSequenceNumber"/> for <see cref="IEvent">events</see>
        /// </summary>
        public Type EventSequenceNumbers { get; set; }
    }
}
