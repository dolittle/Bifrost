using System;
using System.Linq;
using Bifrost.Execution;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Extensions.Decorators;
using SimpleInjector.Advanced;
using SimpleInjector.Advanced.Internal;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using IContainer = Bifrost.Execution.IContainer;
using System;
using System.Collections.Generic;

namespace Bifrost.SimpleInjector
{
    public class Container : IContainer
    {
        readonly global::SimpleInjector.Container _container;

        public Container(global::SimpleInjector.Container container)
        {
            _container = container;
        }
        public BindingLifecycle DefaultLifecycle { get; set; }

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
                if (!optional) throw;
            }

            return default(T);
        }

        public object Get(Type type)
        {
            return _container.GetInstance(type);
        }

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
            _container.Register(service, resolveCallback);
        }

        public void Bind<T>(Func<Type> resolveCallback)
        {
            _container.Register(typeof(T), resolveCallback);
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

        public void Bind(Type service, Func<object> resolveCallback)
        {
            _container.Register(service, resolveCallback);
        }

        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Register<T>(resolveCallback, lifecycle);
        }

        public void Bind(Type service, Func<object> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void Bind<T>(Type type)
        {
            _container.Register(typeof(T), () => type);
        }

        public void Bind(Type service, Type type)
        {
            _container.Register(service, () => type);
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
