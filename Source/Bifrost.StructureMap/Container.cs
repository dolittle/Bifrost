/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Bifrost.Execution;
using System.Collections.Generic;
using StructureMap.Pipeline;

namespace Bifrost.StructureMap
{
	public class Container : IContainer
	{
		global::StructureMap.IContainer _container;

		public Container (global::StructureMap.IContainer container)
		{
			_container = container;
		}

		public T Get<T> ()
		{
			return _container.GetInstance<T>();
		}

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

		public object Get (Type type)
		{
			return _container.GetInstance(type);
		}

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

		public IEnumerable<T> GetAll<T> ()
		{
			return _container.GetAllInstances<T>();
		}

		public bool HasBindingFor (Type type)
		{
			return _container.Model.HasImplementationsFor(type);
		}

		public bool HasBindingFor<T> ()
		{
			return _container.Model.HasImplementationsFor<T>();
		}

		public IEnumerable<object> GetAll (Type type)
		{
			var list = new List<object>();
			foreach( var instance in _container.GetAllInstances(type) ) list.Add (instance);
			return list;
		}

		public IEnumerable<Type> GetBoundServices ()
		{
			return _container.Model.PluginTypes.Select(p=>p.PluginType);
		}

		public void Bind (Type service, Func<Type> resolveCallback)
		{
            _container.Configure (c=>c.For(service).Use((ctx) => resolveCallback()));
		}

		public void Bind<T> (Func<Type> resolveCallback)
		{
            _container.Configure (c=>c.For<T>().UseInstance(new ConfiguredInstance(resolveCallback())));
            
		}

		public void Bind (Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
		{
			_container.Configure (c=>c.For(service).LifecycleIs(GetInstanceScopeFor(lifecycle)).Use(new ConfiguredInstance(resolveCallback())));
		}

		public void Bind<T> (Func<Type> resolveCallback, BindingLifecycle lifecycle)
		{
			_container.Configure (c=>c.For<T>().LifecycleIs(GetInstanceScopeFor(lifecycle)).UseInstance(new ConfiguredInstance(resolveCallback())));
		}

		public void Bind<T> (Type type)
		{
			_container.Configure (c=>c.For<T>().UseInstance(new ConfiguredInstance(type)));
		}

		public void Bind (Type service, Type type)
		{
			_container.Configure (c=>c.For(service).Use(type));
		}

		public void Bind<T> (Type type, BindingLifecycle lifecycle)
		{

            _container.Configure (c=>c.For<T>().LifecycleIs(GetInstanceScopeFor(lifecycle)).UseInstance(new ConfiguredInstance(type)));
		}

		public void Bind (Type service, Type type, BindingLifecycle lifecycle)
		{
			_container.Configure (c=>c.For(service).LifecycleIs(GetInstanceScopeFor(lifecycle)).Use(type));
		}

		public void Bind<T> (T instance)
		{
            _container.Configure (c => c.For(typeof(T)).Use(new ObjectInstance(instance)));
		}

		public void Bind (Type service, object instance)
		{
			_container.Configure (c => c.For(service).Add (instance));
		}


		ILifecycle GetInstanceScopeFor(BindingLifecycle lifecycle)
		{
			switch( lifecycle ) 
			{
			case BindingLifecycle.Transient: return new TransientLifecycle();
            case BindingLifecycle.Request: throw new NotImplementedException();
			case BindingLifecycle.Singleton: return new SingletonLifecycle();
			case BindingLifecycle.Thread: return new ThreadLocalStorageLifecycle();
			}

            return new TransientLifecycle();
		}


        public void Bind<T>(Func<T> resolveCallback)
        {
            throw new NotImplementedException();
        }

        public void Bind(Type service, Func<Type, object> resolveCallback)
        {
            throw new NotImplementedException();
        }

        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public BindingLifecycle DefaultLifecycle { get; set; }
    }
}

