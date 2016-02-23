/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// Indicates that a type used for injecting into an <see cref="IOrderedInstancesOf{T}"/> has a specific
    /// ordering relative to other types decorated with this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class OrderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AfterAttribute"/>.
        /// </summary>
        /// <param name="order">The order of the decorated type.</param>
        public OrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// The order of the decorated type.
        /// </summary>
        /// <remarks>Types without this attribute have order 0.</remarks>
        public int Order { get; private set; }
    }
}