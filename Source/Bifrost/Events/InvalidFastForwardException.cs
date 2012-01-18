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
using System.Runtime.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEventSource">EventSource</see> is stateful 
    /// but there has been an attempt to retrieve it without restoring state by replaying events (fast-forwarding)
    /// </summary>
    public class InvalidFastForwardException : Exception
    {
        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        public InvalidFastForwardException()
        {}

        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public InvalidFastForwardException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="inner">Inner Exception</param>
        public InvalidFastForwardException(string message, Exception inner)
            : base(message, inner)
        {
        }

#if(!SILVERLIGHT)
        /// <summary>
        /// Initializes an <see cref="InvalidFastForwardException">InvalidFastForwardException</see> for serialization
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="context">Streaming Context</param>
        protected InvalidFastForwardException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}