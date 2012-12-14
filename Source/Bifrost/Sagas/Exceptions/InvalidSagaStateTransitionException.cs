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

using System;
using System.Runtime.Serialization;

namespace Bifrost.Sagas.Exceptions
{
    /// <summary>
    /// Exception indicating that the transition between two <see cref="SagaState">SagaStates</see> is invalid.
    /// </summary>
    public class InvalidSagaStateTransitionException : Exception
    {
        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see>
        /// </summary>
        public InvalidSagaStateTransitionException()
            : base()
        {
        }

        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see>
        /// </summary>
        /// <param name="message">Error message</param>
        public InvalidSagaStateTransitionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see>
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Inner Exception</param>
        public InvalidSagaStateTransitionException(string message, Exception inner)
            : base(message, inner)
        {
        }

#if(!SILVERLIGHT && !NETFX_CORE)
        /// <summary>
        /// Initializes an <see cref="InvalidSagaStateTransitionException">InvalidSagaStateTransitionException</see> for serialization
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="context">Streaming Context</param>
        protected InvalidSagaStateTransitionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}