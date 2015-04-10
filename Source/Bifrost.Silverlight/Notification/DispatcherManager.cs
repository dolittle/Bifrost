#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;

namespace Bifrost.Notification
{
    /// <summary>
    /// Represents a manager for managing the dispatcher in an application
    /// </summary>
	public class DispatcherManager
	{
		private static IDispatcher _current;

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
