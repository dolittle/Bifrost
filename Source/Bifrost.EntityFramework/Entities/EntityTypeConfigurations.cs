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
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Bifrost.Execution;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityTypeConfigurations"/>
    /// </summary>
    [Singleton]
    public class EntityTypeConfigurations : IEntityTypeConfigurations
    {
        IDictionary<Type, object> _configurations;

        /// <summary>
        /// Initializes a new instance of <see cref="EntityTypeConfigurations"/>
        /// </summary>
        /// <param name="typeDiscoverer"></param>
        /// <param name="container"></param>
        public EntityTypeConfigurations(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            // Todo: The filtering should not be here when we have a way of excluding assemblies from discovery from extensions
            var configurationTypes = typeDiscoverer.FindMultiple(typeof(EntityTypeConfiguration<>)).Where(t=>!t.FullName.Contains("System."));
            _configurations = configurationTypes.ToDictionary(c=>c, c=>container.Get(c));
        }

#pragma warning disable 1591 // Xml Comments
        public EntityTypeConfiguration<T>   GetFor<T>() where T : class
        {
            var entityType = typeof(T);
            if (!_configurations.ContainsKey(entityType)) return new NullEntityTypeConfiguration<T>();
            return _configurations[entityType] as EntityTypeConfiguration<T>;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
