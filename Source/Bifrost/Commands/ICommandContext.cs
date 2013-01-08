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
using System.Collections.Generic;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Lifecycle;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines a context for a <see cref="ICommand">command</see> passing through
	/// the system
	/// </summary>
	public interface ICommandContext : IUnitOfWork
	{
		/// <summary>
		/// Gets the <see cref="ICommand">command</see> the context is for
		/// </summary>
		ICommand Command { get; }

		/// <summary>
		/// Gets the <see cref="IExecutionContext"/> for the command
		/// </summary>
		IExecutionContext ExecutionContext { get; }

		/// <summary>
		/// Register an aggregated root for tracking
		/// </summary>
		/// <param name="aggregatedRoot">Aggregated root to track</param>
		void RegisterForTracking(IAggregatedRoot aggregatedRoot);

		/// <summary>
		/// Get objects that are being tracked
		/// </summary>
		/// <returns>All tracked objects</returns>
		IEnumerable<IAggregatedRoot> GetObjectsBeingTracked();

        /// <summary>
        /// Get commmitted events for a specific <see cref="EventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="EventSource"/> to get from</param>
        /// <param name="eventSourceId"><see cref="Guid">Id</see> of <see cref="EventSource"/> to get from</param>
        /// <returns><see cref="CommittedEventStream"/> for the <see cref="EventSource"/></returns>
        CommittedEventStream GetCommittedEventsFor(EventSource eventSource, Guid eventSourceId);

        /// <summary>
        /// Returns the last committed <see cref="EventSourceVersion">Event Source Version</see> for the <see cref="EventSource"/>
        /// </summary>
        /// <param name="eventSource"><see cref="EventSource"/> to get <see cref="EventSourceVersion">version</see> for</param>
        /// <param name="eventSourceId"><see cref="Guid">Id</see> of the specific <see cref="EventSource"/></param>
        /// <returns>The last committed <see cref="EventSourceVersion">version</see></returns>
        EventSourceVersion GetLastCommittedVersion(EventSource eventSource, Guid eventSourceId);
	}
}