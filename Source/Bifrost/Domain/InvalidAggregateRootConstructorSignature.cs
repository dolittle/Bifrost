/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Domain
{
    /// <summary>
    /// Gets thrown if an <see cref="AggregateRoot"/> does not follow the convention for expected
    /// signature for the constructor. 
    /// 
    /// Expected format is a public constructor with one parameter which is either a <see cref="Guid"/>
    /// or a <see cref="Events.EventSourceId"/>
    /// </summary>
    public class InvalidAggregateRootConstructorSignature : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidAggregateRootConstructorSignature"/>
        /// </summary>
        /// <param name="type">Type of the <see cref="AggregateRoot"/> that is faulty</param>
        public InvalidAggregateRootConstructorSignature(Type type) :
            base($"Wrong constructor for aggregate root of type '{type.FullName}' - expecting a public constructor taking either a Guid or EventSourceId as the only parameter.")
        { }
    }
}
