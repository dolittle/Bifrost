/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IMappingTargets"/>
    /// </summary>
    public class MappingTargets : IMappingTargets
    {
        static DefaultMappingTarget _defaultMappingTarget = new DefaultMappingTarget();

        IInstancesOf<IMappingTarget> _mappingTargets;
        Dictionary<Type, IMappingTarget> _mappingTargetsByType;

        /// <summary>
        /// Initializes a new instance of <see cref="MappingTargets"/>
        /// </summary>
        /// <param name="mappingTargets"></param>
        public MappingTargets(IInstancesOf<IMappingTarget> mappingTargets)
        {
            _mappingTargets = mappingTargets;
            _mappingTargetsByType = new Dictionary<Type, IMappingTarget>();
            PopulateMappingTargetsByType();
        }

#pragma warning disable 1591 // Xml Comments
        public IMappingTarget GetFor(Type type)
        {
            if (_mappingTargetsByType.ContainsKey(type)) return _mappingTargetsByType[type];

            return _defaultMappingTarget;
        }
#pragma warning restore 1591 // Xml Comments


        void PopulateMappingTargetsByType()
        {
            _mappingTargets.ForEach(mt => _mappingTargetsByType[mt.TargetType] = mt);
        }
    }
}
