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
using Bifrost.EntityFramework.Entities;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Provides fluent configuration extensions for EntityFramework specifics related to entities
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Use EntityFramework as <see cref="IHaveStorage">storage</see>
        /// </summary>
        /// <param name="storage"><see cref="IHaveStorage"/> to configure</param>
        /// <param name="connectionConfiguration">Callback for configuration the connection</param>
        /// <returns></returns>
        public static IConfigure UsingEntityFramework(this IHaveStorage storage, Action<EntityContextConfiguration> connectionConfiguration)
        {
            var configuration = new EntityContextConfiguration();
            connectionConfiguration(configuration);

            var connection = new EntityContextConnection(configuration);
            configuration.Connection = connection;

            storage.EntityContextConfiguration = configuration;
            
            return Configure.Instance;
        }

        /// <summary>
        /// Configure with a given connection string
        /// </summary>
        /// <param name="configuration"><see cref="EntityContextConfiguration"/> to configure</param>
        /// <param name="connectionString">Connection string to use</param>
        /// <returns>Chained <see cref="EntityContextConfiguration"/> to configure</returns>
        public static EntityContextConfiguration WithConnectionString(this EntityContextConfiguration configuration, string connectionString)
        {
            configuration.ConnectionString = connectionString;
            return configuration;
        }
    }
}
