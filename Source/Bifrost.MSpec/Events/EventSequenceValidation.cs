#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Linq;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.MSpec.Events
{
    public class EventSequenceValidation<T> where T : IEvent
    {
        readonly EventStream _stream;

        public EventSequenceValidation(EventStream stream)
        {
            _stream = stream;
        }

        public EventValueValidation<T> AtSequenceNumber(int sequenceNumber)
        {
            var query = from e in _stream
                        where e.Version.Sequence == sequenceNumber
                        select e;

            var @event = (T)query.FirstOrDefault();
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);
        }

        public EventValueValidation<T> InStream()
        {
            var foundEvent = default(T);
            foreach (var @event in _stream)
            {
                if (@event.GetType().Equals(typeof(T)))
                {
                    foundEvent = (T)@event;
                }
            }
            foundEvent.ShouldNotBeNull();
            return new EventValueValidation<T>(foundEvent);


        }

        public EventValueValidation<T> AtBeginning()
        {
            var @event = (T)_stream.FirstOrDefault();
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);
        }

        public EventValueValidation<T> AtEnd()
        {
            var @event = (T)_stream.LastOrDefault();
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);

        }
    }
}