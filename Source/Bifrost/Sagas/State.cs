/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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