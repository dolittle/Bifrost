/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events.InProcess;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the configuration related to <see cref="ICanSendCommittedEventStream"/>
    /// </summary>
    public class CommittedEventStreamSenderConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamSenderConfiguration"/>
        /// </summary>
        public CommittedEventStreamSenderConfiguration()
        {
            CommittedEventStreamSender = typeof(CommittedEventStreamSender);
        }

        /// <summary>
        /// Gets or sets the type of <see cref="ICanSendCommittedEventStream"/> to use for handling sending of <see cref="CommittedEventStream"/>
        /// </summary>
        public Type CommittedEventStreamSender { get; set; }
    }
}