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

using System.Collections.Generic;
using Bifrost.Commands;
using Bifrost.Execution;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for an enumerable of <see cref="IEvent">Events</see> 
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Populates each <see cref="IEvent">Event</see> with the name of the command that caused the event.
        /// </summary>
        /// <param name="events">Enumerable of <see cref="IEvent">events</see> to be extended</param>
        /// <param name="command"><see cref="ICommand">Command</see> that caused the events to be generated</param>
        public static void MarkEventsWithCommandDetails(this IEnumerable<IEvent> events, ICommand command)
        {
            foreach (var @event in events)
            {
                if (string.IsNullOrEmpty(@event.CommandName))
                    @event.CommandName = command == null ? "[Not available]" : command.GetType().Name;

                @event.CommandContext = command.Id;
            }
        }

        /// <summary>
        /// Populates each <see cref="IEvent">Event</see> with elements from the <see cref="IExecutionContext">ExecutionContext</see>
        /// </summary>
        /// <param name="events">Enumerable of <see cref="IEvent">events</see> to be extended</param>
        /// <param name="executionContext"><see cref="IExecutionContext">Execution Context</see> under which the <see cref="IEvent">events</see> were generated</param>
        public static void ExpandExecutionContext(this IEnumerable<IEvent> events, IExecutionContext executionContext)
        {
            if (executionContext == null)
                return;

            foreach (var @event in events)
            {
                @event.CausedBy = executionContext.Principal.Identity.Name;
                @event.Origin = executionContext.System;
            }
        }
    }
}
