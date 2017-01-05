/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Reflection
{
    /// <summary>
    /// Defines a system that needs the instance of a proxy
    /// </summary>
    public interface INeedProxyInstance
    {
        /// <summary>
        /// Gets or sets the instance of the proxy
        /// </summary>
        object Proxy { get; set; }
    }
}
