/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Ninject;
using Ninject.Parameters;
using Ninject.Planning.Bindings;

namespace Bifrost.Ninject
{
    /// <summary>
    /// Represents an implementation of <see cref="IContainer"/> for Ninject
    /// </summary>
    public class Container : IContainer
    {
        readonly List<Type> _boundServices;

        /// <summary>
        /// Initializes a new instance of <see cref="Container"/>
        /// </summary>
        /// <param name="kernel">The <see cref="IKernel"/> used</param>
        public Container(IKernel kernel)
        {
            Kernel = kernel;
            _boundServices = new List<Type>();

#if (!NET461)
            lock (kernel)
            {
                kernel.Components.Remove<global::Ninject.Planning.Strategies.IPlanningStrategy, global::Ninject.Planning.Strategies.ConstructorReflectionStrategy>();
                kernel.Components.Add<global::Ninject.Planning.Strategies.IPlanningStrategy, ConstructorReflectionStrategy>();
            }
#endif
        }

        /// <summary>
        /// Gets the default <see cref="BindingLifecycle"/>
        /// </summary>
        public virtual BindingLifecycle DefaultLifecycle => BindingLifecycle.Transient;

        /// <summary>
        /// Gets the <see cref="IKernel"/> used by the <see cref="Container"/>
        /// </summary>
        public IKernel Kernel { get; }

        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)Get(typeof(T), false);
        }

        /// <inheritdoc/>
        public T Get<T>(bool optional)
        {
            return (T)Get(typeof(T), optional);
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            return Get(type, false);
        }

        /// <inheritdoc/>
        public object Get(Type type, bool optional)
        {
            lock (Kernel)
            {
                var request = Kernel.CreateRequest(type, null, new IParameter[0], optional, true);
                return Kernel.Resolve(request).SingleOrDefault();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetAll<T>()
        {
            lock( Kernel )
                return Kernel.GetAll<T>();
        }

        /// <inheritdoc/>
        public bool HasBindingFor(Type type)
        {
            IEnumerable<IBinding> bindings;
            lock (Kernel)
            {
#if (NET461)
                bindings = Kernel.GetBindings(type);
#else
                bindings = ((IKernelConfiguration)Kernel).GetBindings(type);
#endif
                return bindings.Count() != 0;
            }
        }

        /// <inheritdoc/>
        public bool HasBindingFor<T>()
        {
            return HasBindingFor(typeof (T));
        }

        /// <inheritdoc/>
        public IEnumerable<object> GetAll(Type type)
        {
            lock (Kernel)
                return Kernel.GetAll(type);
        }

        /// <inheritdoc/>
        public IEnumerable<Type> GetBoundServices()
        {
            return _boundServices;
        }

        /// <inheritdoc/>
        public void Bind(Type type, Func<Type> resolveCallback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Bind(Type type, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public void Bind<T>(Type type)
        {
            Bind<T>(type, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type)
        {
            Bind(service, type, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Type type, BindingLifecycle lifecycle)
        {
            lock (Kernel)
            {
                Kernel.Bind<T>().To(type).WithLifecycle(lifecycle);
                _boundServices.Add(typeof(T));
            }
        }

        /// <inheritdoc/>
        public void Bind(Type service, Type type, BindingLifecycle lifecycle)
        {
            lock (Kernel)
            {
                Kernel.Bind(service).To(type).WithLifecycle(lifecycle);
                _boundServices.Add(service);
            }
        }

        /// <inheritdoc/>
        public void Bind<T>(T instance)
        {
            lock (Kernel)
            {
                Kernel.Bind<T>().ToConstant(instance);
                _boundServices.Add(typeof(T));
            }
        }

        /// <inheritdoc/>
        public void Bind(Type service, object instance)
        {
            lock (Kernel)
            {
                Kernel.Bind(service).ToConstant(instance);
                _boundServices.Add(service);
            }
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback)
        {
            Bind(resolveCallback, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback)
        {
            Bind(service, resolveCallback, DefaultLifecycle);
        }

        /// <inheritdoc/>
        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            lock (Kernel)
            {
                Kernel.Bind<T>().ToMethod(c => resolveCallback()).WithLifecycle(lifecycle);
                _boundServices.Add(typeof(T));
            }
        }

        /// <inheritdoc/>
        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            lock (Kernel)
            {
                Kernel.Bind(service).ToMethod(c => resolveCallback(c.Request.Service)).WithLifecycle(lifecycle);
                _boundServices.Add(service);
            }
        }
    }
}
