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
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IMaps"/>
    /// </summary>
    public class Maps : IMaps
    {
        IInstancesOf<IMap> _maps;
        Dictionary<string, IMap> _mapsByKey;


        /// <summary>
        /// Initializes a new instance of <see cref="Maps"/>
        /// </summary>
        /// <param name="maps"><see cref="IInstancesOf{IMap}">Instances of maps</see></param>
        public Maps(IInstancesOf<IMap> maps)
        {
            _maps = maps;
            _mapsByKey = new Dictionary<string, IMap>();
            PopulateMapsBasedOnKeys();
        }

#pragma warning disable 1591 // Xml Comments
        public bool HasFor(Type source, Type target)
        {
            var key = GetKeyFor(source, target);
            return _mapsByKey.ContainsKey(key);
        }

        public IMap GetFor(Type source, Type target)
        {
            ThrowIfMissingMap(source, target);
            var key = GetKeyFor(source,target);
            return _mapsByKey[key];
        }
#pragma warning restore 1591 // Xml Comments

        string GetKeyFor(Type source, Type target)
        {
            return string.Format("{0}_{1}", source.FullName, target.FullName);
        }

        void PopulateMapsBasedOnKeys()
        {
            _maps.ForEach(map => _mapsByKey[GetKeyFor(map.Source, map.Target)] = map);
        }

        void ThrowIfMissingMap(Type source, Type target)
        {
            var key = GetKeyFor(source, target);
            if (!_mapsByKey.ContainsKey(key)) throw new MissingMapException(source, target);
        }
    }
}
