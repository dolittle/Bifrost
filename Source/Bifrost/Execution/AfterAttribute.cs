/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// Indicates that a type used for injecting into an <see cref="IOrderedInstancesOf{T}"/> must be ordered after
    /// another type injected into the same <see cref="IOrderedInstancesOf{T}"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AfterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AfterAttribute"/>.
        /// </summary>
        /// <param name="dependantTypes">The types the decorated type is dependant upon, and must come after.</param>
        public AfterAttribute(params Type[] dependantTypes)
        {
            DependantTypes = dependantTypes;
        }

        /// <summary>
        /// List of types that must be injected before this decorated type is injected.
        /// </summary>
        /// <remarks>This will take precedence over any <see cref="OrderAttribute"/> attributes.</remarks>
        public Type[] DependantTypes { get; set; }
    }
}