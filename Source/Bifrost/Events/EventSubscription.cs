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
using System.Reflection;
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a subscription for a specific <see cref="IEventSubscriber"/> and method on it that can receive a method
    /// </summary>
    public class EventSubscription
    {
        Dictionary<string, EventSourceVersion> _versions = new Dictionary<string, EventSourceVersion>();

        /// <summary>
        /// Gets or sets the owner of the subscriber method that subscribes to the event
        /// </summary>
        public Type Owner { get; set; }

        /// <summary>
        /// Gets or sets the method that is subscribing to the event
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Gets or sets the actual event type that the subscriber handles
        /// </summary>
        public Type EventType { get; set; }

        /// <summary>
        /// Gets or sets the actual event name that the subscriber handles
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets all the versions for the different <see cref="IEventSource"/>s it has handled
        /// </summary>
        public Dictionary<string, EventSourceVersion> Versions 
        {
            get { return _versions; }
            set 
            {
                if (value == null)
                    return;

                foreach (var eventSource in value.Keys)
                    SetEventSourceVersion(eventSource, value[eventSource]);
            }
        }


        /// <summary>
        /// Set the current version for an <see cref="IEventSource"/> if the version is higher than 
        /// what is already on the subscription. If the <see cref="IEventSource"/> is not there,
        /// it will automatically add it
        /// </summary>
        /// <param name="eventSource"><see cref="IEventSource"/> by name to set version for</param>
        /// <param name="version"><see cref="EventSourceVersion"/> to set</param>
        public void SetEventSourceVersion(string eventSource, EventSourceVersion version)
        {
            if (!_versions.ContainsKey(eventSource))
            {
                _versions[eventSource] = version;
                return;
            }

            var currentVersion = _versions[eventSource];
            if (currentVersion.CompareTo(version) < 0)
                _versions[eventSource] = version;
        }

        /// <summary>
        /// Merge versions from another <see cref="EventSubscription"/>
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to merge from</param>
        public void MergeVersionsFrom(EventSubscription subscription)
        {
            foreach( var key in subscription.Versions.Keys )
                SetEventSourceVersion(key, subscription.Versions[key]);
        }

#pragma warning disable 1591 // Xml Comments
        public override bool Equals(object obj)
        {
            var otherSubscription = obj as EventSubscription;

            if (obj == null)
                return false;

            return Owner.Equals(otherSubscription.Owner) &&
                Method.Equals(otherSubscription.Method) &&
                EventName.Equals(otherSubscription.EventName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
#pragma warning restore 1591 // Xml Comments

    }
}
