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
using System.Linq;
using System.Collections.Generic;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a state used in a <see cref="ISaga"/>
    /// </summary>
    [Serializable]
    public class State
    {
        /// <summary>
        /// Holds all states it can transition to
        /// </summary>
        protected readonly IEnumerable<State> _canTransitionTo;

        /// <summary>
        /// Initializes a new instance of <see cref="State"/>
        /// </summary>
        /// <param name="canTransitionTo">Collection of states it can transition to</param>
        protected State(IEnumerable<State> canTransitionTo)
        {
            _canTransitionTo = canTransitionTo;
        }

        /// <summary>
        /// Check if this state can transition to a specified state
        /// </summary>
        /// <param name="state"><see cref="State"/> to check if can transition to</param>
        /// <returns>true if it can transition, false if not</returns>
        public bool CanTransitionTo(State state)
        {
            return GetType() == state.GetType() || _canTransitionTo.Any(s => s.GetType() == state.GetType());
        }
    }
}