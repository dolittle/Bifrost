#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Extensions;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents a mapping description used by <see cref="IMapper"/>
    /// </summary>
    /// <typeparam name="TSource">Source type for the map</typeparam>
    /// <typeparam name="TTarget">Target type for the map</typeparam>
    public abstract class Map<TSource, TTarget> : IMap
    {
        List<PropertyMap<TSource, TTarget>> _properties = new List<PropertyMap<TSource, TTarget>>();

        /// <summary>
        /// Initializes a new instance of <see cref="Map{TSource, TTarget}"/>
        /// </summary>
        public Map()
        {
            AddDefaultPropertyMaps();
        }


        /// <summary>
        /// Describe a specific property
        /// </summary>
        /// <param name="property">Expression representing the property</param>
        /// <returns>A new map for the property</returns>
        protected PropertyMap<TSource, TTarget> Property(Expression<Func<TSource, object>> property)
        {
            var propertyInfo = property.GetPropertyInfo();
            return AddPropertyMap(propertyInfo);
        }


#pragma warning disable 1591 // Xml Comments
        public IEnumerable<PropertyMap<TSource, TTarget>> Properties { get { return _properties; } }

        public Type Source { get { return typeof(TSource); } }

        public Type Target { get { return typeof(TTarget); } }
#pragma warning restore 1591 // Xml Comments

        IEnumerable<IPropertyMap> IMap.Properties { get { return _properties; } }

        void AddDefaultPropertyMaps()
        {
            typeof(TSource).GetTypeInfo().GetProperties().ForEach(p => AddPropertyMap(p).Strategy = new SourcePropertyMappingStrategy(p));
        }

        PropertyMap<TSource, TTarget> AddPropertyMap(System.Reflection.PropertyInfo propertyInfo)
        {
            var propertyMap = new PropertyMap<TSource, TTarget>(propertyInfo);
            _properties.Add(propertyMap);
            return propertyMap;
        }

    }
}