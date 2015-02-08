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

using System.Reflection;
namespace Bifrost.Mapping
{
    /// <summary>
    /// Represents a <see cref="IPropertyMappingStrategy"/> that is typically used when property should map to self - same property as source
    /// </summary>
    /// <remarks>
    /// If the property does not exist in the target, it will just ignore it and the value won't be set. It does not qualify to be an exceptional state.
    /// </remarks>
    public class SourcePropertyMappingStrategy : IPropertyMappingStrategy
    {
        PropertyInfo _propertyInfo;

        /// <summary>
        /// Initializes a new instance of <see cref="SourcePropertyMappingStrategy"/>
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> to base it from</param>
        public SourcePropertyMappingStrategy(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

#pragma warning disable 1591 // Xml Comments
        public void Perform(IMappingTarget mappingTarget, object target, object value)
        {

        }
#pragma warning restore 1591 // Xml Comments
    }
}
