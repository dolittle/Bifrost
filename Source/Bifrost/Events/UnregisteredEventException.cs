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
using System.Runtime.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEvent">Event</see> in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has not been registered as an <see cref="IEvent">Event</see>.
    /// </summary>
    public class UnregisteredEventException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/>
        /// </summary>
        public UnregisteredEventException()
        {}

        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/>
        /// </summary>
        /// <param name="message">Error Message</param>
        public UnregisteredEventException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public UnregisteredEventException(string message, Exception innerException) : base(message,innerException)
        { }

#if(!SILVERLIGHT && !NETFX_CORE)
        /// <summary>
        /// Initializes a <see cref="UnregisteredEventException"/> for serialization
        /// </summary>
        /// <param name="serializationInfo">Serialization Info</param>
        /// <param name="streamingContext">Streaming Context</param>
        protected UnregisteredEventException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo,streamingContext)
        {}
#endif
    }
}