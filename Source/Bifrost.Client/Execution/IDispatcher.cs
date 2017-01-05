/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a dispatcher to be used for checking for thread access on the UI thread and invoking things in the UI thread
    /// </summary>
	public interface IDispatcher
	{
        /// <summary>
        /// Determines wether or not the calling thread is the UI thread
        /// </summary>
        /// <returns>True if the calling thread is the UI thread, false if not</returns>
		bool CheckAccess();

        /// <summary>
        /// Executes asynchronously the delegate on the UI thread
        /// </summary>
        /// <param name="del">Delegate to execute</param>
        /// <param name="arguments">Parameters to pass to the delegate</param>
		void BeginInvoke(Delegate del, params object[] arguments);

        /// <summary>
        /// Executes asynchronously the action on the UI thread
        /// </summary>
        /// <param name="a">Action to execute</param>
		void BeginInvoke(Action a);
	}
}
