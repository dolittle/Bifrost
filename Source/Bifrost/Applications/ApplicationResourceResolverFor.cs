/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanResolveApplicationResources"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IApplicationResourceType"/> it suports</typeparam>
    public abstract class ApplicationResourceResolverFor<T> : ICanResolveApplicationResources
        where T: IApplicationResourceType, new()
    {
        static IApplicationResourceType _resolver = new T();

        /// <inheritdoc/>
        public IApplicationResourceType ApplicationResourceType => _resolver;

        /// <inheritdoc/>
        public abstract Type Resolve(IApplicationResourceIdentifier identifier);
    }
}