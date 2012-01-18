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

using Bifrost.Execution;
using System.Collections.Generic;
using System;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IEventStoreChangeManager">EventStoreChangeManager</see>
    /// </summary>
    [Singleton]
    public class EventStoreChangeManager : IEventStoreChangeManager
    {
        IContainer _container;
        List<Type> _notifiers = new List<Type>();

        /// <summary>
        /// Initializes an instance of <see cref="EventStoreChangeManager">EventStoreChangeManager</see>
        /// </summary>
        /// <param name="container">An instance of the <see cref="IContainer">Container</see> for dependency resolution</param>
        public EventStoreChangeManager(IContainer container)
        {
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
        public void Register(Type type)
        {
            ThrowIfTypeIsNotANotifier(type);
            _notifiers.Add(type);
        }

        public void NotifyChanges(IEventStore eventStore)
        {
            foreach( var notifierType in _notifiers ) 
            {
                var notifier = _container.Get(notifierType) as IEventStoreChangeNotifier;
                notifier.Notify(eventStore);
            }  
        }
#pragma warning restore 1591 // Xml Comments

        void ThrowIfTypeIsNotANotifier(Type type)
        {
            if (!typeof(IEventStoreChangeNotifier).IsAssignableFrom(type))
                throw new ArgumentException(string.Format("Type '{0}' must implement '{1}'", type.Name, typeof(IEventStoreChangeNotifier).Name));
        }
    }
}
