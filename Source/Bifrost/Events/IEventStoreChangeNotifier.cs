/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Events
{
    /// <summary>
    /// Defines a notifier for notifying when changes occur on an <see cref="IEventStore"/>
    /// </summary>
    public interface IEventStoreChangeNotifier
    {
        /// <summary>
        /// Notify changes for a <see cref="IEventStore"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to notify for</param>
        /// <param name="streamOfEvents"><see cref="EventStream"/> with events to notify for</param>
        void Notify(IEventStore eventStore, EventStream streamOfEvents);
    }
}
