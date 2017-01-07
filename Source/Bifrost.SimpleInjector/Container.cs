/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;

namespace Bifrost.SimpleInjector
{
    public class Container : IContainer
    {
        readonly global::SimpleInjector.Container _container;

        public Container(global::SimpleInjector.Container container)
        {
            _container = container;
        }

        public virtual BindingLifecycle DefaultLifecycle => BindingLifecycle.Transient;

        public T Get<T>()
        {
            return (T)_container.GetInstance(typeof(T));
        }

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

        public object Get(Type type)
        {
            return _container.GetInstance(type);
        }

        public object Get(Type type, bool optional)
        {
            try
            {
                return _container.GetInstance(type);
            }
            catch
            {
                if (!optional)
                {
                    throw;
                }
            }

            return null;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _container.GetAllInstances<T>();
        }

        public bool HasBindingFor(Type type)
        {
            return _container.GetAllInstances(type).Count() != 0;
        }

        public bool HasBindingFor<T>()
        {
            return _container.GetAllInstances(typeof(T)).Count() != 0;
        }

        public IEnumerable<object> GetAll(Type type)
        {
            return _container.GetAllInstances(type);
        }

        public IEnumerable<Type> GetBoundServices()
        {
            return _container.GetCurrentRegistrations()
                 .Select(r => r.ServiceType)
                 .AsEnumerable();
        }

        public void Bind(Type service, Func<Type> resolveCallback)
        {
            _container.Register(service, resolveCallback, DefaultLifecycle);
        }

        public void Bind<T>(Func<Type> resolveCallback)
        {
            _container.Register(typeof(T), resolveCallback, DefaultLifecycle);
        }

        public void Bind(Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register(service, resolveCallback, lifecycle);
        }

        public void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register<T>(resolveCallback, lifecycle);
        }

        public void Bind<T>(Func<T> resolveCallback)
        {
            Func<object> objectResolver = () => resolveCallback;
            _container.Register(typeof(T), objectResolver);
        }

        public void Bind(Type service, Func<Type, object> resolveCallback)
        {
            _container.Register(service, resolveCallback, DefaultLifecycle);
        }

        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register(resolveCallback, lifecycle);
        }

        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register(service, resolveCallback, lifecycle);
        }

        public void Bind<T>(Type type)
        {
            _container.Register(typeof(T), () => type, DefaultLifecycle);
        }

        public void Bind(Type service, Type type)
        {
            _container.Register(service, () => type, DefaultLifecycle);
        }

        public void Bind<T>(Type type, BindingLifecycle lifecycle)
        {
            _container.Register(typeof(T), () => type, lifecycle);
        }

        public void Bind(Type service, Type type, BindingLifecycle lifecycle)
        {
            _container.Register(service, () => type, lifecycle);
        }

        public void Bind<T>(T instance)
        {
            _container.Register(typeof(T),() => instance);
        }

        public void Bind(Type service, object instance)
        {
            _container.Register(service, () => instance);
        }

    }
}
