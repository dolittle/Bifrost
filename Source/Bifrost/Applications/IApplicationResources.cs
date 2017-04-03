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
        /// Identify a resource from its instance
        /// </summary>
        /// <param name="resource">Resource to identify</param>
        /// <returns><see cref="IApplicationResourceIdentifier"/> identifying the resource</returns>
        IApplicationResourceIdentifier Identify(object resource);

        /// <summary>
        /// Identify a resource from its type
        /// </summary>
        /// <param name="type">Type of the resource to identify</param>
        /// <returns><see cref="IApplicationResourceIdentifier"/> identifying the resource</returns>
        IApplicationResourceIdentifier Identify(Type type);
    }
}
