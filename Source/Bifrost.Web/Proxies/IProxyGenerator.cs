/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Conventions;

namespace Bifrost.Web.Proxies
{
    /// <summary>
    /// Defines a system that can generate javascript proxies.
    /// </summary>
    /// <remarks>
    /// Types implementing this interface will be automatically registered and invoked during proxy generation.
    /// </remarks>
    public interface IProxyGenerator : IConvention
    {
        string Generate();
    }
}
