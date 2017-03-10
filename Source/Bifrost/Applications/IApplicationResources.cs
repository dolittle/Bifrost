/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a utility for working with application resources
    /// </summary>
    public interface IApplicationResources
    {
        /// <summary>
        /// Identify a resource
        /// </summary>
        /// <param name="resource">Resource to identify</param>
        /// <returns><see cref="ApplicationResourceIdentifier"/> identifying the resource</returns>
        ApplicationResourceIdentifier Identify(object resource);
    }
}
