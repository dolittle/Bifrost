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