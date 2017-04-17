/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using IContainer = Bifrost.Execution.IContainer;

namespace Bifrost.SimpleInjector
{
    /// <summary>
    /// Represents an implementation of <see cref="IContainer"/> for SimpleInjector
    /// </summary>
    public class Container : IContainer
    {
        global::SimpleInjector.Container _container;

        /// <summary>
        /// Initializes a new instance of <see cref="IContainer"/>
        /// </summary>
        /// <param name="container">SimpleInjectors <see cref="global::SimpleInjector.Container"/></param>
        public Container(global::SimpleInjector.Container container)
        {
            _container = container;
        }

        /// <summary>
        /// Gets the default <see cref="BindingLifecycle"/>
        /// </summary>
        public virtual BindingLifecycle DefaultLifecycle => BindingLifecycle.Transient;

        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)_container.GetInstance(typeof(T));
        }

        /// <inheritdoc/>
        public T Get<T>(bool optional)
        {
            try
            {
                return (T)_container.GetInstance(typeof(T));
            }
            catch
            {
                if (!optional)
                {
                    throw;
                }
            }

            return default(T);
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            return _container.GetInstance(type);
        }

        /// <inheritdoc/>
        public object Get(Type type, bool optional)
        {
            try
            {
                return _container.GetInstance(type);
            }
            catch
            {
                if (!optional) throw;
            }

            return null;
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetAll<T>()
        {
            return _container.GetAllInstances(typeof(T)) as IEnumerable<T>;
        }

        /// <inheritdoc/>
        public bool HasBindingFor(Type type)
        {
            return _container.GetAllInstances(type).Count() != 0;
        }

        /// <inheritdoc/>
        public bool HasBindingFor<T>()
        {
            return _container.GetAllInstances(typeof(T)).Count() != 0;
        }

        /// <inheritdoc/>
        public IEnumerable<object> GetAll(Type type)
        {
            return _container.GetAllInstances(type);
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetBoundServices()
        {
            return _container.GetCurrentRegistrations()
                 .Select(r => r.ServiceType)
                 .AsEnumerable();
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type> resolveCallback)
        {
            _container.Register(service, resolveCallback, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback)
        {
            _container.Register(typeof(T), resolveCallback, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register(service, resolveCallback, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register<T>(resolveCallback, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback)
        {
            Func<object> objectResolver = () => resolveCallback;
            _container.Register(typeof(T), objectResolver);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback)
        {
            _container.Register(service, resolveCallback, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register(resolveCallback, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register(service, resolveCallback, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Type type)
        {
            _container.Register(typeof(T), () => type, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type)
        {
            _container.Register(service, () => type, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Type type, BindingLifecycle lifecycle)
        {
            _container.Register(typeof(T), () => type, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type, BindingLifecycle lifecycle)
        {
            _container.Register(service, () => type, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(T instance)
        {
            _container.Register(typeof(T),() => instance);
        }

        /// <inheritdoc/>
        public void Bind(Type service, object instance)
        {
            _container.Register(service, () => instance);
        }
    }
}
