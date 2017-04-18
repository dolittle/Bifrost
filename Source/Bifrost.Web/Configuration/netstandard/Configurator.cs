/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;

namespace Bifrost.Web.Configuration
{
    /// <summary>
    /// Represents a <see cref="ICanConfigure">configurator</see> specific for the Web project
    /// </summary>
    public class Configurator : ICanConfigure
    {
        /// <inheritdoc/>
        public void Configure(IConfigure configure)
        {
            configure.CallContext.WithCallContextTypeOf<WebCallContext>();
        }
    }
}
