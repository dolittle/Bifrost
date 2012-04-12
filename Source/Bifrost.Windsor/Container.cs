using System;
using System.Linq;
using System.Collections.Generic;
using Bifrost.Execution;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
	
namespace Bifrost.Windsor
{
	public class Container : IContainer
	{
		IWindsorContainer	_windsorContainer;
		
		public Container(IWindsorContainer windsorContainer)
		{
			_windsorContainer = windsorContainer;
		}
		
		
		public T Get<T> ()
		{
			return _windsorContainer.Resolve<T>();
		}

		public T Get<T> (bool optional)
		{
            return (T)Get(typeof(T), optional);
		}

		public object Get (Type type)
		{
			return _windsorContainer.Resolve (type);
		}

		public object Get (Type type, bool optional)
		{
            // Todo : Figure out a way to resolve types "optionally"
            try
            {
                return _windsorContainer.Resolve(type);
            }
            catch(Exception ex)
            {
                if (!optional)
                    throw ex;
                else
                    return null;
            }
		}


		public bool HasBindingFor (Type type)
		{
			// Todo : figure out a way to figure out wether or not there is a registration or not
			try {
				_windsorContainer.Resolve(type);
				return true;
			} catch {
				return false;
			}
		}

		public bool HasBindingFor<T> ()
		{
			return HasBindingFor(typeof(T));
		}

		public IEnumerable<T> GetAll<T> ()
		{
			return _windsorContainer.ResolveAll<T>();
		}

		public IEnumerable<object> GetAll (Type type)
		{
			return (IEnumerable<object>)_windsorContainer.ResolveAll (type);
		}

		public IEnumerable<Type> GetBoundServices ()
		{
			var services = _windsorContainer.Kernel.GetAssignableHandlers(typeof(object))
				.Select(h=>h.ComponentModel.Service);
			
			return services;
			
		}

		public void Bind (Type service, Func<Type> resolveCallback)
		{
			throw new NotImplementedException ();
		}

		public void Bind<T> (Func<Type> resolveCallback)
		{
			throw new NotImplementedException ();
		}

		public void Bind (Type service, Func<Type> resolveCallback, BindingLifecycle lifecycle)
		{
			throw new NotImplementedException ();
		}

		public void Bind<T> (Func<Type> resolveCallback, BindingLifecycle lifecycle)
		{
			throw new NotImplementedException ();
		}

		public void Bind<T> (Type type)
		{
			Bind (typeof(T),type);
		}

		public void Bind (Type service, Type type)
		{
			_windsorContainer.Register (Component.For(service).ImplementedBy(type));
		}

		public void Bind<T> (Type type, BindingLifecycle lifecycle)
		{
			Bind (typeof(T), type, lifecycle);
		}

		public void Bind (Type service, Type type, BindingLifecycle lifecycle)
		{
			_windsorContainer.Register (Component.For(service).ImplementedBy(type).WithLifecycle(lifecycle));
		}

		public void Bind<T> (T instance)
		{
			Bind (typeof(T), instance);
		}

		public void Bind (Type service, object instance)
		{
			_windsorContainer.Register(Component.For(service).Instance(instance));
		}
	}
}

