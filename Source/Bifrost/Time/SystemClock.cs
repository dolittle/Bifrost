#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;

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
        public static readonly DateTime MinimumTime = new DateTime(1900, 1, 1);

        private static DateTime? _explicitNow;

        /// <summary>
        /// Retrieves the current system date and time
        /// </summary>
        /// <returns>The current system date and time</returns>
        public static DateTime GetCurrentTime()
        {
            return _explicitNow.GetValueOrDefault(DateTime.Now);
        }

        /// <summary>
        /// Allows the current date and time to be set to an explicit value.  SOLELY FOR TESTING PURPOSES.
        /// Use within a "using" block within your test so that the current time is reset on exiting the test.
        /// </summary>
        /// <param name="dateTime">The explicit datetime that you wish to set within the test</param>
        /// <returns>A new instance of the SystemClock class which will return the explicitly set current time when queried.</returns>
        public static IDisposable SetNowTo(DateTime dateTime)
        {
            _explicitNow = dateTime;
            return new SystemClock();
        }

        /// <summary>
        /// Will remove any explicitly set current time,
        /// </summary>
        public void Dispose()
        {
            _explicitNow = null;
        }
    };
}