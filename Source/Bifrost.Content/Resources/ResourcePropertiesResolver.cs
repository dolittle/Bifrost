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
using System.Linq;
using Bifrost.Extensions;
using Bifrost.Execution;

namespace Bifrost.Content.Resources
{
    /// <summary>
    /// Represents a <see cref="IResourcePropertiesResolver"/>
    /// </summary>
    public class ResourcePropertiesResolver : IResourcePropertiesResolver
    {
		IContainer _container;
		
        /// <summary>
        /// Initializes a new instance of <see cref="ResourcePropertiesResolver"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to use</param>
        public ResourcePropertiesResolver(IContainer container)
        {
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
        public void ResolvePropertiesFor<T>(T instance)
        {
            var propertiesQuery = from t in typeof (T).GetProperties()
                                  where t.PropertyType.HasInterface<IHaveResources>()
                                  select t;
            foreach (var property in propertiesQuery )
            {
                var stringsInstance = _container.Get(property.PropertyType);
                property.SetValue(instance, stringsInstance, null);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}