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
using System.Collections.Generic;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a state used in a <see cref="ISaga"/>
    /// </summary>
    public class State
    {
        // Todo : Get rid of ICollection, we should probably expose this differently


        /// <summary>
        /// Holds all states it can transition to
        /// </summary>
        protected readonly ICollection<State> m_canTransitionTo;

        /// <summary>
        /// Initializes a new instance of <see cref="State"/>
        /// </summary>
        /// <param name="canTransitionTo">Collection of states it can transition to</param>
        protected State(ICollection<State> canTransitionTo)
        {
            m_canTransitionTo = canTransitionTo;
        }

        /// <summary>
        /// Check if this state can transition to a specified state
        /// </summary>
        /// <param name="state"><see cref="State"/> to check if can transition to</param>
        /// <returns>true if it can transition, false if not</returns>
        public bool CanTransitionTo(State state)
        {
            return m_canTransitionTo.Contains(state);
        }
    }
}