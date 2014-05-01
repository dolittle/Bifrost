#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
