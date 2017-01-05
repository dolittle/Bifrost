/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Sagas.Exceptions;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents the state of a saga
    /// </summary>
    public class SagaState
    {
        /// <summary>
        /// The <see cref="Concluded"/> state of a <see cref="ISaga"/>
        /// </summary>
        public static readonly State CONCLUDED = new Concluded();

        /// <summary>
        /// The <see cref="Continuing"/> state of a <see cref="ISaga"/>
        /// </summary>
        public static readonly State CONTINUING = new Continuing();

        /// <summary>
        /// The <see cref="Begun"/> state of a <see cref="ISaga"/>
        /// </summary>
        public static readonly State BEGUN = new Begun();

        /// <summary>
        /// The <see cref="New"/> state of a <see cref="ISaga"/>
        /// </summary>
        public static readonly State NEW = new New();

        State _currentState;

        /// <summary>
        /// Initializes a new instance of <see cref="SagaState"/> with a current state
        /// </summary>
        /// <param name="currentState"><see cref="State">Current state</see> to set</param>
        public SagaState(State currentState)
        {
            _currentState = currentState;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SagaState"/> with the current state set to <see cref="New"/>
        /// </summary>
        public SagaState() : this(NEW)
        {}

        /// <summary>
        /// Check if a transition is allowed
        /// </summary>
        /// <param name="newState"><see cref="State"/> to check if is allowed</param>
        /// <returns>true if transition is allowed, false if not</returns>
        public bool CanTransitionTo(State newState)
        {
            return _currentState.CanTransitionTo(newState);
        }

        /// <summary>
        /// Transition to a given state
        /// </summary>
        /// <param name="newState"><see cref="State"/> to transition to</param>
        /// <exception cref="InvalidSagaStateTransitionException">Thrown if transition is not allowed</exception>
        public void TransitionTo(State newState)
        {
            if(!CanTransitionTo(newState))
                throw new InvalidSagaStateTransitionException(string.Format("Cannot transition from State [{0}] to State [{1}]",_currentState.GetType().FullName, newState.GetType().FullName));

            _currentState = newState;
        }

        /// <summary>
        /// Get wether or not the current state is <see cref="New"/>
        /// </summary>
        public bool IsNew { get { return _currentState == NEW; } }

        /// <summary>
        /// Get wether or not the current state is <see cref="Continuing"/>
        /// </summary>
        public bool IsContinuing { get { return _currentState == CONTINUING; } }

        /// <summary>
        /// Get wether or not the current state is <see cref="Begun"/>
        /// </summary>
        public bool IsBegun { get { return _currentState == BEGUN; } }

        /// <summary>
        /// Get wether or not the current state is <see cref="Concluded"/>
        /// </summary>
        public bool IsConcluded { get { return _currentState == CONCLUDED; } }

        /// <summary>
        /// Implicit operator for converting a <see cref="string"/> to a <see cref="SagaState"/>
        /// </summary>
        /// <param name="state"><see cref="string"/> containing state - see remarks</param>
        /// <returns><see cref="SagaState"/> for the state string</returns>
        /// <remarks>
        /// Supported strings : 
        /// new
        /// begun
        /// continuing
        /// concluded
        /// 
        /// Any other strings will cause a <see cref="UnknownSagaStateException"/>
        /// </remarks>
        public static implicit operator SagaState(string state)
        {
            switch (state)
            {
                case Constants.BEGUN:
                    return new SagaState(SagaState.BEGUN);
                case Constants.CONTINUING:
                    return new SagaState(SagaState.CONTINUING);
                case Constants.NEW:
                    return new SagaState(SagaState.NEW);
                case Constants.CONCLUDED:
                    return new SagaState(SagaState.CONCLUDED);
                default:
                    throw new UnknownSagaStateException(string.Format("Cannot set a Saga State of {0}", state));
            }
        }

        /// <summary>
        /// Outputs the current state as string
        /// </summary>
        /// <returns>A string representing the current state</returns>
        public override string ToString()
        {
            return _currentState.ToString();
        }

        class Concluded : State
        {
            public Concluded()
                : base(new List<State>() { })
            {
            }

            public override string ToString()
            {
                return Constants.CONCLUDED;
            }
        }

        class Continuing : State
        {
            public Continuing()
                : base(new List<State>() { CONCLUDED })
            {
                m_canTransitionTo.Add(this);
            }

            public override string ToString()
            {
                return Constants.CONTINUING;
            }
        }

        class Begun : State
        {
            public Begun()
                : base(new List<State>() { SagaState.CONTINUING, SagaState.CONCLUDED })
            {
            }

            public override string ToString()
            {
                return Constants.BEGUN;
            }
        }

        class New : State
        {
            public New()
                : base(new List<State>() { SagaState.BEGUN })
            {
            }

            public override string ToString()
            {
                return Constants.NEW;
            }
        }
    }
}