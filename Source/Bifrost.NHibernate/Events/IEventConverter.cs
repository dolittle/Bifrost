/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Events;

namespace Bifrost.NHibernate.Events
{
    /// <summary>
    /// Defines a converter for converting an <see cref="IEvent"/> to <see cref="EventHolder"/> and vice versa
    /// </summary>
	public interface IEventConverter
	{
        /// <summary>
        /// Convert an <see cref="EventHolder"/> to an <see cref="IEvent"/> of correct type from the <see cref="EventHolder"/>
        /// </summary>
        /// <param name="eventHolder"><see cref="EventHolder"/> to convert</param>
        /// <returns>A new instance of an <see cref="IEvent"/> with correct type based upon the <see cref="EventHolder"/></returns>
		IEvent ToEvent(EventHolder eventHolder);

        /// <summary>
        /// Converts an <see cref="IEvent"/> to an <see cref="EventHolder"/>
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> to convert</param>
        /// <returns>A new instance of an <see cref="EventHolder"/> holding all details about the <see cref="IEvent"/></returns>
		EventHolder ToEventHolder(IEvent @event);

        /// <summary>
        /// Convert an <see cref="IEvent"/> into an existing <see cref="EventHolder"/>
        /// </summary>
        /// <param name="eventHolder"><see cref="EventHolder"/> to convert into</param>
        /// <param name="event"><see cref="IEvent"/> to convert from</param>
		void ToEventHolder(EventHolder eventHolder, IEvent @event);

        /// <summary>
        /// Converts an <see cref="IEnumerable{IEvent}"/> into an <see cref="IEnumerable{EventHolder}"/>
        /// </summary>
        /// <param name="events">Events to convert</param>
        /// <returns>Converted <see cref="EventHolder"/>s</returns>
        IEnumerable<EventHolder> ToEventHolders(IEnumerable<IEvent> events);

        /// <summary>
        /// Converts an <see cref="IEnumerable{EventHolder}"/> into an <see cref="IEnumerable{IEvent}"/>
        /// </summary>
        /// <param name="eventHolders"><see cref="EventHolder"/>s to convert</param>
        /// <returns>Converted events</returns>
        IEnumerable<IEvent> ToEvents(IEnumerable<EventHolder> eventHolders);
	}
}