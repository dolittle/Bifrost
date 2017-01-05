/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Exception that gets thrown when a TargetInstance property for implementations
    /// of <see cref="INeedTargetInstance"/> mismatches the type expected for <see cref="ICanHandleInvocationsFor{T1,T2}"/>
    /// </summary>
    public class TargetInstanceTypeMismatch : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TargetInstanceTypeMismatch"/>
        /// </summary>
        /// <param name="targetInstanceType"><see cref="Type"/> of the target instance</param>
        /// <param name="expectedType"><see cref="Type"/> that is expected</param>
        public TargetInstanceTypeMismatch(Type targetInstanceType, Type expectedType) : base(string.Format(Strings.TargetInstanceTypeMismatch, targetInstanceType, expectedType)) { }
    }
}
