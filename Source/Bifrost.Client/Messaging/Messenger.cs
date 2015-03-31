#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;
using Bifrost.Execution;

namespace Bifrost.Messaging
{
    /// <summary>
    /// Represents an implementation of <see cref="IMessenger"/>
    /// </summary>
    [Singleton]
    public class Messenger : IMessenger
    {
        Dictionary<Type, List<WeakDelegate>> _subscribers = new Dictionary<Type, List<WeakDelegate>>();

#pragma warning disable 1591
        public void Publish<T>(T content)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                var forRemoval = new List<WeakDelegate>();

                foreach (var subscriber in _subscribers[type])
                {
                    if (subscriber.IsAlive)
                    {
                        subscriber.DynamicInvoke(content);
                    }
                    else
                    {
                        forRemoval.Add(subscriber);
                    }
                }

                forRemoval.ForEach(s => _subscribers[type].Remove(s));
            }
        }

        public void SubscribeTo<T>(Action<T> receivedCallback)
        {
            var type = typeof(T);
            List<WeakDelegate> subscribersList = null;
            if (_subscribers.ContainsKey(type))
                subscribersList = _subscribers[type];
            else
            {
                subscribersList = new List<WeakDelegate>();
                _subscribers[type] = subscribersList;
            }
            subscribersList.Add(new WeakDelegate(receivedCallback));
        }
#pragma warning restore 1591
    }
}
