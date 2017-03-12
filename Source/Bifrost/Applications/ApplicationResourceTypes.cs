/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResourceTypes"/>
    /// </summary>
    [Singleton]
    public class ApplicationResourceTypes : IApplicationResourceTypes
    {
        Dictionary<string, IApplicationResourceType> _resourceTypesByIdentifier;
        Dictionary<Type, IApplicationResourceType> _resourceTypesByType;


        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResourceTypes"/>
        /// </summary>
        /// <param name="applicationResourceTypes"></param>
        public ApplicationResourceTypes(IInstancesOf<IApplicationResourceType> applicationResourceTypes)
        {
            var all = applicationResourceTypes.ToArray();
            _resourceTypesByIdentifier = all.ToDictionary(r => r.Identifier, r => r);
            _resourceTypesByType = all.ToDictionary(r => r.Type, r => r);
        }


        /// <ineritdoc/>
        public IApplicationResourceType GetFor(string identifier)
        {
            ThrowIfUnknownIdentifier(identifier);

            var resourceType = _resourceTypesByIdentifier[identifier];
            return resourceType;
        }

        /// <ineritdoc/>
        public IApplicationResourceType GetFor(Type type)
        {
            var resourceType = _resourceTypesByType.Where(r => type.Implements(r.Key)).Select(r => r.Value).SingleOrDefault();
            ThrowIfUnknownType(type, resourceType);

            return resourceType;
        }


        void ThrowIfUnknownIdentifier(string identifier)
        {
            if (!_resourceTypesByIdentifier.ContainsKey(identifier))
                throw new UnknownApplicationResourceType(identifier);
        }

        void ThrowIfUnknownType(Type type, IApplicationResourceType resourceType)
        {
            if (resourceType == null)
                throw new UnknownApplicationResourceType(type);
        }

    }
}
