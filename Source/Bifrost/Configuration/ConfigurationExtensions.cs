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
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Security;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Provides Configuration extensions
	/// </summary>
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// Configures the running system with a name
        /// </summary>
        /// <param name="configure"><see cref="IConfigure"/> instance to configure</param>
        /// <param name="name">Name of the system</param>
        /// <returns>Chained <see cref="IConfigure"/> instance</returns>
        public static IConfigure WithSystemName(this IConfigure configure, string name)
        {
            configure.SystemName = name;
            return configure;
        }

        /// <summary>
        /// Configures events to be persisted synchronously
        /// </summary>
        /// <param name="configuration"><see cref="IEventsConfiguration"/> instance to configure</param>
        /// <param name="configurationAction">Callback for further configuring the <see cref="IEventsConfiguration"/></param>
        /// <returns>Chained <see cref="IConfigure"/> instance</returns>
        public static IConfigure Synchronous(this IEventsConfiguration configuration, Action<IEventsConfiguration> configurationAction = null)
        {
            configuration.UncommittedEventStreamCoordinatorType = typeof(UncommittedEventStreamCoordinator);
            if (configurationAction != null)
                configurationAction(configuration);
            return Configure.Instance;
        }

        /// <summary>
        /// Configures events to be persisted asynchronously
        /// </summary>
        /// <param name="configuration"><see cref="IEventsConfiguration"/> instance to configure</param>
        /// <param name="configurationAction">Callback for further configuring the <see cref="IEventsConfiguration"/></param>
        /// <returns>Chained <see cref="IConfigure"/> instance</returns>
        public static IConfigure Asynchronous(this IEventsConfiguration configuration, Action<IEventsConfiguration> configurationAction = null)
        {
            configuration.UncommittedEventStreamCoordinatorType = typeof(AsynchronousUncommittedEventStreamCoordinator);
            if (configurationAction != null)
                configurationAction(configuration);
            return Configure.Instance;
        }


        /// <summary>
        /// Binds given entity context for a specific type (IEntityContext of T)
        /// </summary>
        /// <typeparam name="T">The Type that this vbinding will work for</typeparam>
        /// <param name="configuration">EntityContextConfiguration instance</param>
        /// <param name="container">Container</param>
        public static void BindEntityContextTo<T>(this IEntityContextConfiguration configuration, IContainer container)
        {
            BindEntityContextConfigurationInstance(configuration, container);

            var source = typeof(IEntityContext<>).MakeGenericType(typeof(T));
            var target = configuration.EntityContextType.MakeGenericType(typeof(T));
            container.Bind(source, target);
        }

        /// <summary>
        /// Binds given entity context as default IEntityContext
        /// </summary>
        /// <param name="configuration">EntityContextConfiguration instance</param>
        /// <param name="container">Container</param>
        public static void BindDefaultEntityContext(this IEntityContextConfiguration configuration, IContainer container)
        {
            BindEntityContextConfigurationInstance(configuration, container);
            container.Bind(typeof(IEntityContext<>), configuration.EntityContextType);
        }


        /// <summary>
        /// Configure what <see cref="ICallContext"/> to use
        /// </summary>
        /// <typeparam name="T">Type of use as <see cref="ICallContext"/></typeparam>
        /// <param name="callContextConfiguration"><see cref="ICallContextConfiguration"/> to configure</param>
        public static void WithCallContextTypeOf<T>(this ICallContextConfiguration callContextConfiguration) where T : ICallContext
        {
            callContextConfiguration.CallContextType = typeof(T);
        }
        
        static void BindEntityContextConfigurationInstance(IEntityContextConfiguration configuration, IContainer container)
        {
            var connectionType = configuration.Connection.GetType();

            if(!container.HasBindingFor(connectionType))
                container.Bind(connectionType, configuration.Connection);
        }
    }
}
