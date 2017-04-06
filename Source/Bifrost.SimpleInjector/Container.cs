/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Bifrost.Execution;
using SimpleInjector;
using IContainer = Bifrost.Execution.IContainer;
using System.Collections.Generic;

namespace Bifrost.SimpleInjector
{
    /// <summary>
    /// Represents an implementation of <see cref="IContainer"/> for SimpleInjector
    /// </summary>
    public class Container : IContainer
    {
        readonly global::SimpleInjector.Container _container;

        /// <summary>
        /// Initializes a new instance of <see cref="IContainer"/>
        /// </summary>
        /// <param name="container">SimpleInjectors <see cref="global::SimpleInjector.Container"/></param>
        public Container(global::SimpleInjector.Container container)
        {
            _container = container;
        }

        /// <inheritdoc/>
        public BindingLifecycle DefaultLifecycle { get; set; }

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
                if (!optional) throw;
            }

            return default(T);
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            return _container.GetInstance(type);
        }

        /// <inheritdoc/>
        public object Get(Type type, bool optional = false)
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
            _container.Register(service, resolveCallback);
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback)
        {
            _container.Register(typeof(T), resolveCallback);
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
            _container.Register(service, () => resolveCallback(service));
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register<T>(resolveCallback, lifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Bind<T>(Type type)
        {
            _container.Register(typeof(T), () => type);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type)
        {
            _container.Register(service, () => type);
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
