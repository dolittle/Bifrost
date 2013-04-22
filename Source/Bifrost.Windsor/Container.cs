#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
			_windsorContainer.Register (Component.For<DefaultTypeLoader>());
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
            var handlers = _windsorContainer.Kernel.GetHandlers(type);

            return _windsorContainer.Kernel.GetAssignableHandlers(typeof(object))
                    .Where(h=>h.ComponentModel.Service == type)
                    .Any();
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
			_windsorContainer.Register (Component.For(service).Forward(type).ImplementedBy(type));
		}

		public void Bind<T> (Type type, BindingLifecycle lifecycle)
		{
			Bind (typeof(T), type, lifecycle);
		}

		public void Bind (Type service, Type type, BindingLifecycle lifecycle)
		{
			_windsorContainer.Register (Component.For(service).Forward(type).ImplementedBy(type).WithLifecycle(lifecycle));
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

