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

namespace Bifrost.Events
{
    /// <summary>
	/// Defines the basics of an event
	/// </summary>
	public interface IEvent
	{
		/// <summary>
		/// Gets the id of the event
		/// </summary>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the Id of CommandContext in which the event originated from
        /// </summary>
        Guid CommandContext { get; set; }

		/// <summary>
		/// Gets or sets the name of the command causing the event
		/// </summary>
		string CommandName { get; set; }

		/// <summary>
		/// Gets or sets the name of the event
		/// </summary>
		string Name { get; set; }

        /// <summary>
        /// Gets the EventSource id (Aggregate Root) to which these events belong.
        /// </summary>
		Guid EventSourceId { get; set; }

		/// <summary>
		/// Gets and sets the eventsource
		/// </summary>
    	string EventSource { get; set; }

        /// <summary>
        /// Gets or sets the version of the event (ChangeSet or something)
        /// </summary>
        EventSourceVersion Version { get; set; }

		/// <summary>
		/// Gets or sets who or what the event was caused by.
		/// 
		/// Typically this would be the name of the user or system causing it
		/// </summary>
		string CausedBy { get; set; }

		/// <summary>
		/// Gets or sets the origin of the event.
		/// 
		/// Typically this would be what part of the system the event indirectly is coming from
		/// </summary>
		string Origin { get; set; }

		/// <summary>
		/// Gets or sets the time the event occured
		/// </summary>
		DateTime Occured { get; set; }
	}
}
