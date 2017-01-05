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
    /// </summary>
    /// <typeparam name="TInterface"><see cref="Type"/> of interface</typeparam>
    /// <typeparam name="TImplementation"><see cref="Type"/> of implementation</typeparam>
    public interface ICanHandleInvocationsFor<TInterface, TImplementation> : ICanHandleInvocations
        where TImplementation : TInterface
    {
    }
}
