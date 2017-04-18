/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;
using SimpleInjector;

namespace Bifrost.SimpleInjector
{
    /// <summary>
    /// Extensions for <see cref="global::SimpleInjector.Container"/>
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Register a binding of a type to a callback that can resolve an instance of the type with a given lifecycle
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <param name="container"><see cref="global::SimpleInjector.Container"/> to register into</param>
        /// <param name="resolveCallback"><see cref="Func{T}"/> that resolves the type by returning an instance</param>
        /// <param name="lifecycle"><see cref="BindingLifecycle">Lifecycle</see> of the binding</param>
        public static void Register<T>(this global::SimpleInjector.Container container, Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            Func<Type> typeResolver = () => { return resolveCallback.Invoke().GetType(); };
            container.Register(typeof(T), typeResolver, lifecycle);
        }

        /// <summary>
        /// Register a binding of a type to a callback that can resolve it with a given lifecycle
        /// </summary>
        /// <typeparam name="T">Type to register</typeparam>
        /// <param name="container"><see cref="global::SimpleInjector.Container"/> to register into</param>
        /// <param name="resolveCallback"><see cref="Func{T}"/> that resolves the type</param>
        /// <param name="lifecycle"><see cref="BindingLifecycle">Lifecycle</see> of the binding</param>
        public static void Register<T>(this global::SimpleInjector.Container container, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            container.Register(typeof(T), resolveCallback, lifecycle);
        }

        /// <summary>
        /// Register a binding of a type to a callback that can resolve an implementation of the type with a given lifecycle
        /// </summary>
        /// <param name="container"><see cref="global::SimpleInjector.Container"/> to register into</param>
        /// <param name="service"><see cref="Type"/> to register</param>
        /// <param name="resolveCallback"><see cref="Func{T}"/> that resolves the type</param>
        /// <param name="lifecycle"><see cref="BindingLifecycle">Lifecycle</see> of the binding</param>
        public static void Register(this global::SimpleInjector.Container container, Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            var lifestyle = ResolveLifestyle(lifecycle);
            container.Register(service, resolveCallback, lifestyle);
        }


        /// <summary>
        /// Register a binding of a type to a callback that can resolve an instance of the type with a given lifecycle
        /// </summary>
        /// <param name="container"><see cref="global::SimpleInjector.Container"/> to register into</param>
        /// <param name="service"><see cref="Type"/> to register</param>
        /// <param name="resolveCallback"><see cref="Func{T}"/> that resolves the instance</param>
        /// <param name="lifecycle"><see cref="BindingLifecycle">Lifecycle</see> of the binding</param>
        public static void Register(this global::SimpleInjector.Container container, Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            var lifestyle = ResolveLifestyle(lifecycle);
            container.Register(service, () => resolveCallback, lifestyle);
        }

        static Lifestyle ResolveLifestyle(BindingLifecycle lifecycle)
        {
            var lifestyle = Lifestyle.Transient;
            switch (lifecycle)
            {
                case BindingLifecycle.Singleton:
                    lifestyle = Lifestyle.Singleton;
                    break;
                case BindingLifecycle.Request:
                    throw new NotImplementedException();
                case BindingLifecycle.Transient:
                    lifestyle = Lifestyle.Transient;
                    break;
                case BindingLifecycle.Thread:
                    throw new NotSupportedException("Ref documentation: This lifestyle is deliberately left out of Simple Injector because it is considered to be harmful. Instead of using Per Thread lifestyle, you will usually be better of using one of the Scoped lifestyles.");
            }

            return lifestyle;
        }
    }
}
