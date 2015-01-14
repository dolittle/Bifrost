#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
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
