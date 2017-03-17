/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines the configuration for events
    /// </summary>
    public interface IEventsConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Gets or sets the type of <see cref="IUncommittedEventStreamCoordinator"/> used for coordinating events that will be committed
        /// </summary>
        Type UncommittedEventStreamCoordinator { get; set; }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventStore"/> to use for handling events
        /// </summary>
        Type EventStore { get; set; }

        /// <summary>
        /// Gets or sets the type of <see cref="ICanReceiveCommittedEventStream"/> to use for handling sending of <see cref="CommittedEventStream"/>
        /// </summary>
        Type CommittedEventStreamSender { get; set; }

        /// <summary>
        /// Gets or sets the type of <see cref="ICanReceiveCommittedEventStream"/> to use for handling receiving of <see cref="CommittedEventStream"/>
        /// </summary>
        Type CommittedEventStreamReceiver { get; set; }

        /// <summary>
        /// Gets the configuration for <see cref="IEventSequenceNumbers"/>
        /// </summary>
        EventSequenceConfiguration EventSequenceNumbers { get; }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventProcessorLog"/> to use for logging
        /// </summary>
        Type EventProcessorLog { get; set; }
    }
}