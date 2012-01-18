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
using Bifrost.Events;
using Microsoft.WindowsAzure.StorageClient;

namespace Bifrost.Azure.Events
{
	/// <summary>
	/// Represents an entity used for persisting events in an <see cref="IEventStore">EventStore</see>
	/// </summary>
	public class PersistentEvent : TableServiceEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PersistentEvent">PersistentEvent</see>
		/// </summary>
		public PersistentEvent(object aggregatedRootKey)
		{
			PartitionKey = aggregatedRootKey.ToString();
			RowKey = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Gets or sets the aggregated roots Id that the event applies to
		/// </summary>
		public Guid AggregateId { get; set; }

		/// <summary>
		/// Gets or sets the name of the event
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the event type
		/// 
		/// A string representation of the typename
		/// </summary>
		public string EventType { get; set; }

		/// <summary>
		/// Gets or sets the aggregated root type the event applies to.
		/// 
		/// A string representation of the typename
		/// </summary>
		public string AggregatedRootType { get; set; }

		/// <summary>
		/// Gets or sets the serialized event.
		/// 
		/// This is a Json representation of the actual event
		/// </summary>
		public string SerializedEvent { get; set; }

		/// <summary>
		/// Gets or sets who or what the event was caused by.
		/// 
		/// Typically this would be the name of the user or system causing it
		/// </summary>
		public string CausedBy { get; set; }

		/// <summary>
		/// Gets or sets the origin of the event.
		/// 
		/// Typically this would be what part of the system the event indirectly is coming from
		/// </summary>
		public string Origin { get; set; }

		/// <summary>
		/// Gets or sets the time the event occured
		/// </summary>
		public DateTime Occured { get; set; }
	}
}
