/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines the configuration for events
    /// </summary>
    public interface IEventsConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Gets the configuration for <see cref="ICanSendCommittedEventStream"/>
        /// </summary>
        CommittedEventStreamSenderConfiguration CommittedEventStreamSender { get; }

        /// <summary>
        /// Gets the configuration for <see cref="ICanReceiveCommittedEventStream"/>
        /// </summary>
        CommittedEventStreamReceiverConfiguration CommittedEventStreamReceiver { get; }

        /// <summary>
        /// Gets the configuration for <see cref="IEventStore"/>
        /// </summary>
        EventStoreConfiguration EventStore { get; }

        /// <summary>
        /// Gets the configuration for <see cref="IEventSourceVersions"/>
        /// </summary>
        EventSourceVersionsConfiguration EventSourceVersions { get; }

        /// <summary>
        /// Gets the configuration for <see cref="IEventSequenceNumbers"/>
        /// </summary>
        EventSequenceNumbersConfiguration EventSequenceNumbers { get; }

        /// <summary>
        /// Gets the configuration for <see cref="IEventProcessorStates"/>
        /// </summary>
        EventProcessorStatesConfiguration EventProcessorStates { get; }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventProcessorLog"/> to use for logging
        /// </summary>
        Type EventProcessorLog { get; set; }
    }
}