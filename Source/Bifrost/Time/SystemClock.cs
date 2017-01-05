/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Time
{
    /// <summary>
    /// Represents a clock that keeps track of the current system date and time.
    /// Current system date and time can be substituted for explicit DateTime for testing purposes.
    /// </summary>
    public class SystemClock : IDisposable
    {
        /// <summary>
        /// Gets the minimum time supported by the <see cref="SystemClock"/>
        /// </summary>
        public static readonly DateTime MinimumTime = new DateTime(1900, 1, 1,0, 0, 0, DateTimeKind.Utc);

         static Stack<DateTime?> _explicitNows = new Stack<DateTime?>();

        /// <summary>
        /// Retrieves the current system date and time
        /// </summary>
        /// <returns>The current system date and time</returns>
        public static DateTime GetCurrentTime()
        {
            return _explicitNows.Count == 0 ? DateTime.UtcNow : _explicitNows.Peek().GetValueOrDefault(DateTime.UtcNow);
        }

        /// <summary>
        /// Allows the current date and time to be set to an explicit value.  SOLELY FOR TESTING PURPOSES.
        /// Use within a "using" block within your test so that the current time is reset on exiting the test.
        /// </summary>
        /// <param name="dateTime">The explicit datetime that you wish to set within the test</param>
        /// <returns>A new instance of the SystemClock class which will return the explicitly set current time when queried.</returns>
        public static IDisposable SetNowTo(DateTime dateTime)
        {
            _explicitNows.Push(dateTime);
            return new SystemClock();
        }

        /// <summary>
        /// Will remove any explicitly set current time,
        /// </summary>
        public void Dispose()
        {
            if(_explicitNows.Count > 0)
                _explicitNows.Pop();
        }
    };
}