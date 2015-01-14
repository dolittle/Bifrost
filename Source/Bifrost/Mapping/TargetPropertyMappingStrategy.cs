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
using System.Reflection;

namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyMappingStrategy"/> that supports mapping to 
    /// a specified property
    /// </summary>
    public class TargetPropertyMappingStrategy : IPropertyMappingStrategy
    {

        /// <summary>
        /// Initializes a new instance of <see cref="TargetPropertyMappingStrategy"/>
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> representing the property</param>
        public TargetPropertyMappingStrategy(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> representing the property
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public void Perform(IMappingTarget mappingTarget, object target, object sourceValue)
        {
            mappingTarget.SetValue(target, PropertyInfo, sourceValue);
        }
#pragma warning restore 1591 // Xml Comments

    }
}
