/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
    /// <summary>
	/// Defines the basics of an event
	/// </summary>
	public interface IEvent
	{
		/// <summary>
		/// Gets or sets the id of the event
		/// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the Id of CommandContext in which the event originated from
        /// </summary>
        Guid CommandContext { get; set; }

		/// <summary>
		/// Gets or sets the name of the command causing the event
		/// </summary>
		string CommandName { get; set; }

		/// <summary>
		/// Gets or sets the name of the event
		/// </summary>
		string Name { get; set; }

        /// <summary>
        /// Gets the EventSource id (Aggregate Root) to which these events belong.
        /// </summary>
		Guid EventSourceId { get; set; }

		/// <summary>
		/// Gets and sets the eventsource
		/// </summary>
    	string EventSource { get; set; }

        /// <summary>
        /// Gets or sets the version of the event (ChangeSet or something)
        /// </summary>
        EventSourceVersion Version { get; set; }

		/// <summary>
		/// Gets or sets who or what the event was caused by.
		/// 
		/// Typically this would be the name of the user or system causing it
		/// </summary>
		string CausedBy { get; set; }

		/// <summary>
		/// Gets or sets the origin of the event.
		/// 
		/// Typically this would be what part of the system the event indirectly is coming from
		/// </summary>
		string Origin { get; set; }

		/// <summary>
		/// Gets or sets the time the event occured
		/// </summary>
		DateTime Occured { get; set; }
	}
}
