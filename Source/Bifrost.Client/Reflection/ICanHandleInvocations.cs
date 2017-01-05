/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Castle.DynamicProxy;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Defines a system that can handle invocations from an interface and delegate it to a concrete
    /// instance
    public interface ICanHandleInvocations
    {
        /// <summary>
        /// Gets asked for wether or not it can handle a specific <see cref="IInvocation"/>
        /// </summary>
        /// <param name="invocation"><see cref="IInvocation"/> to ask for</param>
        /// <returns>True if it can handle it, false if not</returns>
        bool CanHandle(IInvocation invocation);

        /// <summary>
        /// Handel a specific <see cref="IInvocation"/>
        /// </summary>
        /// <param name="invocation"><see cref="IInvocation"/> to handle</param>
        void Handle(IInvocation invocation);

    }
}
