/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a mapping target - more specifically you should look at <see cref="IMappingTargetFor"/>
    /// </summary>
    public interface IMappingTarget
    {
        /// <summary>
        /// Gets the type of target object
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// Set value for a member with a given value
        /// </summary>
        /// <param name="target">Target object to set value for</param>
        /// <param name="member"><see cref="MemberInfo"/> to set for</param>
        /// <param name="value">Actual value to set</param>
        void SetValue(object target, MemberInfo member, object value);
    }
}