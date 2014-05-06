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
namespace Bifrost.Messaging
{
    public class Messenger : IMessenger
    {
        Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public void Publish<T>(T content)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                foreach (var subscriber in _subscribers[type])
                    subscriber.DynamicInvoke(content);
            }
        }

        public void SubscribeTo<T>(Action<T> receivedCallback)
        {
            var type = typeof(T);
            List<Delegate> subscribersList = null;
            if (_subscribers.ContainsKey(type))
                subscribersList = _subscribers[type];
            else
            {
                subscribersList = new List<Delegate>();
                _subscribers[type] = subscribersList;
            }
            subscribersList.Add(receivedCallback);
        }
    }
}
