/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
	/// <summary>
	/// Defines an invoker for handle methods - it should recognize methods called Handle and be able to 
	/// call them
	/// </summary>
	/// <remarks>
	/// This is a convention were a type implementing methods called Handle taking specific commands in.
	/// </remarks>
	public interface IProcessMethodInvoker
	{
		/// <summary>
		/// Try to call handle method for a specific command
		/// </summary>
		/// <param name="instance">Instance to try to call Handle method on</param>
		/// <param name="event">The <see cref="IEvent"/> that the Process method should take</param>
		/// <returns>True if it called the Handle method, false if not</returns>
		bool TryProcess(object instance, IEvent @event);

		/// <summary>
		/// Register a type that should have Handle method(s) in it
		/// </summary>
		/// <param name="typeWithProcessMethods">Type to register</param>
		void Register(Type typeWithProcessMethods);
	}
}