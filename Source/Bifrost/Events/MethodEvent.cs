/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Bifrost.Events
{
	/// <summary>
	/// Represents an event that represents a method on a domain object
	/// </summary>
	public class MethodEvent : Event
	{
		private readonly MethodInfo _methodInfo;

		/// <summary>
		/// Constructs a MethodEvent based upon a MethodInfo
		/// </summary>
		/// <param name="eventSourceId">Id of the event source</param>
		/// <param name="methodInfo"></param>
		public MethodEvent(Guid eventSourceId, MethodInfo methodInfo) : base(eventSourceId)
		{
			_methodInfo = methodInfo;
			Name = _methodInfo.Name;
			Arguments = new MethodEventArguments();
		}

		/// <summary>
		/// Gets the arguments for the method
		/// </summary>
		public dynamic Arguments { get; private set; }
	}
}
