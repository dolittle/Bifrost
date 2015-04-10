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

namespace Bifrost.Messaging
{
    /// <summary>
    /// Defines a messenger that provides a publish / subscribe bus
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Publish a message of a given type
        /// </summary>
        /// <typeparam name="T">Type of message to publish</typeparam>
        /// <param name="content">Message to publish</param>
        void Publish<T>(T content);

        /// <summary>
        /// Subscribe to a given message by its type
        /// </summary>
        /// <typeparam name="T">Type to subscribe to</typeparam>
        /// <param name="receivedCallback"><see cref="Action{T}"/> that gets called when a message is received</param>
        void SubscribeTo<T>(Action<T> receivedCallback);
    }
}
