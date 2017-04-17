/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using StructureMap.Pipeline;

namespace Bifrost.StructureMap
{
    /// <summary>
    /// Represents an implementation of <see cref="IContainer"/> for StructureMap
    /// </summary>
    public class Container : IContainer
    {
        global::StructureMap.IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="Container"/>
        /// </summary>
        /// <param name="container">StructureMaps <see cref="global::StructureMap.IContainer"/></param>
        public Container (global::StructureMap.IContainer container)
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
            return _container.GetInstance<T>();
        }

        /// <inheritdoc/>
        public T Get<T> (bool optional)
        {
            try 
            {
                return _container.GetInstance<T>();
            } 
            catch 
            {
                if( !optional ) throw;
            }

            return default(T);
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            return _container.GetInstance(type);
        }


        /// <inheritdoc/>
        public object Get (Type type, bool optional = false)
        {
            try 
            {
                return _container.GetInstance (type);
            }
            catch
            {
                if( !optional ) throw;
            }

            return null;
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetAll<T>()
        {
            return _container.GetAllInstances<T>();
        }

        /// <inheritdoc/>
        public bool HasBindingFor(Type type)
        {
            return _container.Model.HasImplementationsFor(type);
        }

        /// <inheritdoc/>
        public bool HasBindingFor<T>()
        {
            return _container.Model.HasImplementationsFor<T>();
        }


        /// <inheritdoc/>
        public IEnumerable<object> GetAll(Type type)
        {
            var list = new List<object>();
            foreach( var instance in _container.GetAllInstances(type) ) list.Add (instance);
            return list;
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetBoundServices()
        {
            return _container.Model.PluginTypes.Select(p=>p.PluginType);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type> resolveCallback)
        {
            _container.Configure (c=>c.For(service).Use((ctx) => resolveCallback()));
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback)
        {
            _container.Configure (c=>c.For<T>().UseInstance(new ConfiguredInstance(resolveCallback())));
            
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Configure (c=>c.For(service).LifecycleIs(GetInstanceScopeFor(lifecycle)).Use(new ConfiguredInstance(resolveCallback())));
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Configure (c=>c.For<T>().LifecycleIs(GetInstanceScopeFor(lifecycle)).UseInstance(new ConfiguredInstance(resolveCallback())));
        }

        /// <inheritdoc/>
        public void Bind<T>(Type type)
        {
            _container.Configure (c=>c.For<T>().UseInstance(new ConfiguredInstance(type)));
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type)
        {
            _container.Configure (c=>c.For(service).Use(type));
        }

        /// <inheritdoc/>
        public void Bind<T>(Type type, BindingLifecycle lifecycle)
        {

            _container.Configure (c=>c.For<T>().LifecycleIs(GetInstanceScopeFor(lifecycle)).UseInstance(new ConfiguredInstance(type)));
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type, BindingLifecycle lifecycle)
        {
            _container.Configure (c=>c.For(service).LifecycleIs(GetInstanceScopeFor(lifecycle)).Use(type));
        }

        /// <inheritdoc/>
        public void Bind<T>(T instance)
        {
            _container.Configure (c => c.For(typeof(T)).Use(new ObjectInstance(instance)));
        }

        /// <inheritdoc/>
        public void Bind(Type service, object instance)
        {
            _container.Configure (c => c.For(service).Add (instance));
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback)
        {
            _container.Configure(c => c.For<T>().Use(ctx => resolveCallback()));
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback)
        {
            _container.Configure(c => c.For(service).Use(ctx => resolveCallback(service)));
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Configure(c => c.For<T>().LifecycleIs(GetInstanceScopeFor(lifecycle)).Use(ctx => resolveCallback()));
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            _container.Configure(c => c.For(service).LifecycleIs(GetInstanceScopeFor(lifecycle)).Use(ctx => resolveCallback(service)));
        }


        ILifecycle GetInstanceScopeFor(BindingLifecycle lifecycle)
        {
            switch (lifecycle)
            {
                case BindingLifecycle.Transient: return new TransientLifecycle();
                case BindingLifecycle.Request: throw new NotImplementedException();
                case BindingLifecycle.Singleton: return new SingletonLifecycle();
                case BindingLifecycle.Thread: return new ThreadLocalStorageLifecycle();
            }

            return new TransientLifecycle();
        }
    }
}
