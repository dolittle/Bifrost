#region License
//
// Copyright (c) 2008-2011, DoLittle Studios and Komplett ASA
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
using System.Linq;
using Bifrost.Events;
using Bifrost.Time;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Bifrost.Azure.Events
{
	public class EventStore : IEventStore
	{
		static CloudStorageAccount _storageAccount;
		static string _connectionString;

		public static string ConnectionString
		{
			get { return _connectionString; }
			set
			{
				_connectionString = value;
				_storageAccount = CloudStorageAccount.Parse(value);

				CloudTableClient.CreateTablesFromModel(
						typeof(PersistentEventContext),
						_storageAccount.TableEndpoint.AbsoluteUri,
						_storageAccount.Credentials
					);
			}
		}

		PersistentEventContext _context;

		public EventStore()
		{
			_context = new PersistentEventContext(_storageAccount.TableEndpoint.AbsoluteUri, _storageAccount.Credentials)
			           	{
			           		RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1))
			           	};
		}


		public CommittedEventStream Load(Type aggregatedRootType, Guid aggregateId)
		{
			var query = from e in _context.Events
						where e.PartitionKey == aggregateId.ToString()
						select e;

			var events = query.ToArray();
			var convertedEvents = events.Select(Convert);
			var stream = new CommittedEventStream(aggregateId);
			stream.Append(convertedEvents);
			return stream;
		}

		public void Save(Type aggregatedRootType, Guid aggregateId, UncommittedEventStream eventsToSave)
		{
			var eventEntities = eventsToSave.Select(e => Convert(aggregatedRootType, aggregateId, e));
			foreach (var @event in eventEntities)
			{
				_context.Insert(@event);
			}
			_context.Commit();
		}

		private static PersistentEvent Convert(Type aggregatedRootType, Guid aggregateId, IEvent @event)
		{
			var eventType = @event.GetType();
			var entity = new PersistentEvent(aggregateId)
			{
				AggregateId = aggregateId,
				AggregatedRootType = aggregatedRootType.AssemblyQualifiedName,
				EventType = eventType.AssemblyQualifiedName,
				SerializedEvent = @event.ToJson(),
				Name = eventType.Name,
				Occured = SystemClock.GetCurrentTime(),
			};

			return entity;
		}

		private static IEvent Convert(PersistentEvent persistentEvent)
		{
			var eventType = Type.GetType(persistentEvent.EventType);
			var @event = EventSerializationExtensions.FromJson(eventType, persistentEvent.SerializedEvent);
			return @event;
		}
	}
}
