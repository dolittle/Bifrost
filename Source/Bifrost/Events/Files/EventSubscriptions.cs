/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Bifrost.Serialization;

namespace Bifrost.Events.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptions"/>
    /// </summary>
    public class EventSubscriptions : IEventSubscriptions
    {
        EventStoreConfiguration _configuration;
        ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSubscriptions"/>
        /// </summary>
        /// <param name="configuration"><see cref="EventStoreConfiguration"/> to use for configuration</param>
        /// <param name="serializer"><see cref="ISerializer"/> to use for serialization</param>
        public EventSubscriptions(EventStoreConfiguration configuration, ISerializer serializer)
        {
            _configuration = configuration;
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments

        string GetPathForSubscriptions()
        {
            var fullPath = Path.Combine(_configuration.Path, "EventSubscribers");
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            return fullPath;
        }

        class EventSubscriptionHolder
        {
            public string Id { get; set; }
            public string Owner { get; set; }
            public string EventType { get; set; }
            public string EventName { get; set; }
            public long LastEventId { get; set; }
        }

        public IEnumerable<EventSubscription> GetAll()
        {
            var subscriptions = new List<EventSubscription>();
            var path = GetPathForSubscriptions();
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var holder = _serializer.FromJson<EventSubscriptionHolder>(json);
                var subscription = new EventSubscription();
                subscription.Id = Guid.Parse(holder.Id);
                subscription.LastEventId = holder.LastEventId;
                subscription.Owner = Type.GetType(holder.Owner);
                subscription.EventType = Type.GetType(holder.EventType);
                subscription.EventName = holder.EventName;
                subscription.Method = subscription.Owner.GetTypeInfo().GetMethod(Bifrost.Events.ProcessMethodInvoker.ProcessMethodName, new Type[] { subscription.EventType });
                subscriptions.Add(subscription);
            }

            return subscriptions;
        }

        public void Save(EventSubscription subscription)
        {
            var path = GetPathForSubscriptions();
            var file = Path.Combine(path,$"{subscription.Owner.Namespace}.{subscription.Owner.Name}.{subscription.EventName}");

            var holder = new EventSubscriptionHolder
            {
                Id = subscription.Id.ToString(),
                LastEventId = subscription.LastEventId,
                Owner = string.Format("{0}.{1}, {2}", subscription.Owner.Namespace, subscription.Owner.Name, subscription.Owner.GetTypeInfo().Assembly.GetName().Name),
                EventType = string.Format("{0}.{1}, {2}", subscription.EventType.Namespace, subscription.EventType.Name, subscription.EventType.GetTypeInfo().Assembly.GetName().Name),
                EventName = subscription.EventName
            };

            var json = _serializer.ToJson(holder);
            File.WriteAllText(file, json);
        }

        public void ResetLastEventForAllSubscriptions()
        {
            var path = GetPathForSubscriptions();
            Directory.Delete(path, true);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
