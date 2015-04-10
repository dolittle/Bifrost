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
using System.Linq.Expressions;
using Bifrost.Extensions;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Extends <see cref="PropertyMap"/> with mapping utilities
    /// </summary>
    public static class PropertyMapExtensions
    {
        /// <summary>
        /// Map a property to another property in the target
        /// </summary>
        /// <typeparam name="TSource">Type of the source to map from</typeparam>
        /// <typeparam name="TTarget">Type of the target to map to</typeparam>
        /// <param name="propertyMap">The <see cref="PropertyMap{TSource,TTarget}"/></param>
        /// <param name="propertyExpression">Expression representing the property</param>
        /// <returns>The propertymap for fluent interfaces</returns>
        public static PropertyMap<TSource, TTarget> To<TSource, TTarget>(this PropertyMap<TSource, TTarget> propertyMap, Expression<Func<TTarget, object>> propertyExpression)
        {
            var property = propertyExpression.GetPropertyInfo();
            propertyMap.Strategy = new TargetPropertyMappingStrategy(property);
            return propertyMap;
        }
    }
}
