/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a manager for notifying changes to registered notifiers
    /// </summary>
    public interface IEventStoreChangeManager
    {
        /// <summary>
        /// Register a <see cref="IEventStoreChangeNotifier"/> that can be notified when changes occur in an event store
        /// </summary>
        /// <param name="type">Type of notifier to register, it must however implement the <see cref="IEventStoreChangeNotifier"/> interface</param>
        void Register(Type type); 

        /// <summary>
        /// Notify changes for a given <see cref="IEventStore"/>
        /// </summary>
        void NotifyChanges(IEventStore eventStore, EventStream streamOfEvents);
    }
}
