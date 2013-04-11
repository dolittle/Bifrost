#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
    /// has not implemented the correct <see cref="Bifrost.Events.IAmNextGenerationOf{T}">interface</see>.
    /// </summary>
    public class NotAMigratedEventTypeException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        public NotAMigratedEventTypeException()
        {}

        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public NotAMigratedEventTypeException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public NotAMigratedEventTypeException(string message, Exception innerException) : base(message,innerException)
        { }

#if(!SILVERLIGHT && !NETFX_CORE)
        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see> for serialization
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected NotAMigratedEventTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo,streamingContext)
        {}
#endif
    }
}