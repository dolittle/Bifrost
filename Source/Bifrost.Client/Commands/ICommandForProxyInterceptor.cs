/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Castle.DynamicProxy;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines the interceptor for <see cref="ICommandForProxyInterceptor"/>
    /// </summary>
    public interface ICommandForProxyInterceptor : IInterceptor
    {
    }
}
