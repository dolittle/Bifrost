/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a manager for managing the dispatcher in an application
    /// </summary>
	public class DispatcherManager
	{
		static IDispatcher _current;

        /// <summary>
        /// Gets wether or not the dispatcher has been set
        /// </summary>
        public static bool HasBeenSet { get { return _current != null;  } }


        /// <summary>
        /// Gets or sets the current <see cref="IDispatcher"/>
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="ArgumentException"/> when getting and no <see cref="IDispatcher"/> has been set
        /// </remarks>
		public static IDispatcher Current
		{
			get
			{
				if( null == _current )
				{
					throw new ArgumentException("Current Dispatcher has not been set in DispatcherManager");
				}
				return _current;
			}
			set
			{
				_current = value;
			}
		}
	}
}
