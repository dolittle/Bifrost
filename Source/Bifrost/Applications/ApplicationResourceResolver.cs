/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResourceResolver"/>
    /// </summary>
    [Singleton]
    public class ApplicationResourceResolver : IApplicationResourceResolver
    {
        Dictionary<string, ICanResolveApplicationResources> _resolversByType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolvers"></param>
        public ApplicationResourceResolver(IInstancesOf<ICanResolveApplicationResources> resolvers)
        {
            _resolversByType = resolvers.ToDictionary(r => r.ApplicationResourceType.Identifier, r => r);
        }

        /// <inheritdoc/>
        public Type Resolve(IApplicationResourceIdentifier identifier)
        {
            var typeIdentifier = identifier.Resource.Type.Identifier;
            if (_resolversByType.ContainsKey(typeIdentifier)) return _resolversByType[typeIdentifier].Resolve(identifier);

            throw new UnknownApplicationResourceType(identifier.Resource.Type.Identifier);
        }
    }
}
