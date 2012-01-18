#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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

namespace Bifrost.Events
{
	/// <summary>
	/// Defines a repository that holds events generated
	/// </summary>
	public interface IEventStore
	{
		/// <summary>
		/// Load events for a specific aggregated root 
		/// </summary>
		/// <param name="aggregatedRootType">Type of aggregated root</param>
		/// <param name="aggregateId">Id of the aggregated root</param>
		/// <returns>All events for the aggregated root in an Event Stream</returns>
		CommittedEventStream Load(Type aggregatedRootType, Guid aggregateId);

		/// <summary>
		/// Save events for a specific aggregated root
		/// </summary>
		/// <param name="eventsToSave">Events to save as an Event Stream</param>
		void Save(UncommittedEventStream eventsToSave);

        /// <summary>
        /// Returns the last committed <see cref="EventSourceVersion">Event Source Version</see> for the aggregate root
        /// </summary>
        /// <param name="aggregatedRootType">Type of the aggregrate root</param>
        /// <param name="aggregateId">Id of the aggregate root</param>
        /// <returns></returns>
	    EventSourceVersion GetLastCommittedVersion(Type aggregatedRootType, Guid aggregateId);
	}
}