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
using Bifrost.Events;

namespace Bifrost.NHibernate.Events
{
    /// <summary>
    /// Defines a converter for converting an <see cref="IEvent"/> to <see cref="EventHolder"/> and vice versa
    /// </summary>
	public interface IEventConverter
	{
        /// <summary>
        /// Convert an <see cref="EventHolder"/> to an <see cref="IEvent"/> of correct type from the <see cref="EventHolder"/>
        /// </summary>
        /// <param name="eventHolder"><see cref="EventHolder"/> to convert</param>
        /// <returns>A new instance of an <see cref="IEvent"/> with correct type based upon the <see cref="EventHolder"/></returns>
		IEvent ToEvent(EventHolder eventHolder);

        /// <summary>
        /// Converts an <see cref="IEvent"/> to an <see cref="EventHolder"/>
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> to convert</param>
        /// <returns>A new instance of an <see cref="EventHolder"/> holding all details about the <see cref="IEvent"/></returns>
		EventHolder ToEventHolder(IEvent @event);

        /// <summary>
        /// Convert an <see cref="IEvent"/> into an existing <see cref="EventHolder"/>
        /// </summary>
        /// <param name="eventHolder"><see cref="EventHolder"/> to convert into</param>
        /// <param name="event"><see cref="IEvent"/> to convert from</param>
		void ToEventHolder(EventHolder eventHolder, IEvent @event);

        /// <summary>
        /// Converts an <see cref="IEnumerable{IEvent}"/> into an <see cref="IEnumerable{EventHolder}"/>
        /// </summary>
        /// <param name="events">Events to convert</param>
        /// <returns>Converted <see cref="EventHolder"/>s</returns>
        IEnumerable<EventHolder> ToEventHolders(IEnumerable<IEvent> events);

        /// <summary>
        /// Converts an <see cref="IEnumerable{EventHolder}"/> into an <see cref="IEnumerable{IEvent}"/>
        /// </summary>
        /// <param name="eventHolders"><see cref="EventHolder"/>s to convert</param>
        /// <returns>Converted events</returns>
        IEnumerable<IEvent> ToEvents(IEnumerable<EventHolder> eventHolders);
	}
}