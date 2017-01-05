/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

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
        Type UncommittedEventStreamCoordinatorType { get; set; }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventStore"/> to use for handling events
        /// </summary>
        Type EventStoreType { get; set; }

        /// <summary>
        /// Gets or sets the type of <see cref="IEventSubscriptions"/> to use for handling event subscriptions
        /// </summary>
        Type EventSubscriptionsType { get; set; }

        /// <summary>
        /// Add a <see cref="IEventStoreChangeNotifier"/> type for the configuration
        /// </summary>
        /// <param name="type"></param>
        void AddEventStoreChangeNotifier(Type type);
    }
}