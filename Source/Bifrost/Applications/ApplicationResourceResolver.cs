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
    public class ApplicationResourceResolver : IApplicationResourceResolver
    {
        Dictionary<IApplicationResourceType, ICanResolveApplicationResources> _resolversByType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolvers"></param>
        public ApplicationResourceResolver(IInstancesOf<ICanResolveApplicationResources> resolvers)
        {
            _resolversByType = resolvers.ToDictionary(r => r.ApplicationResourceType, r => r);
        }

        /// <inheritdoc/>
        public Type Resolve(IApplicationResourceIdentifier identifier)
        {


            throw new NotImplementedException();
        }
    }
}
