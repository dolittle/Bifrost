/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events.InProcess;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the configuration related to <see cref="ICanReceiveCommittedEventStream"/>
    /// </summary>
    public class CommittedEventStreamReceiverConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamReceiverConfiguration"/>
        /// </summary>
        public CommittedEventStreamReceiverConfiguration()
        {
            CommittedEventStreamReceiver = typeof(CommittedEventStreamReceiver);
        }

        /// <summary>
        /// Gets or sets the type of <see cref="ICanReceiveCommittedEventStream"/> to use for handling sending of <see cref="CommittedEventStream"/>
        /// </summary>
        public Type CommittedEventStreamReceiver { get; set; }
    }
}