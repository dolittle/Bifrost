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
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventSubscriptions"/>
    /// </summary>
    public class NullEventSubscriptions : IEventSubscriptions
    {
#pragma warning disable 1591 // Xml Comments
        public IEnumerable<EventSubscription> GetAll()
        {
            return new EventSubscription[0];
        }

        public void Save(EventSubscription subscription)
        {

        }

        public void ResetLastEventForAllSubscriptions()
        {
        }
#pragma warning restore 1591 // Xml Comments


    }
}
