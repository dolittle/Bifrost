/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Execution;

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
