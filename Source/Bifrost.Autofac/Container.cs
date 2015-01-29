#region License

//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core; 
using Bifrost.Execution;
using IContainer = Bifrost.Execution.IContainer;

namespace Bifrost.Autofac
{
    /// <summary>
    /// Represents an implementation of <see cref="IContainer"/> for AutoFac
    /// </summary>
    public class Container : IContainer
    {
        global::Autofac.IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="Container"/>
        /// </summary>
        /// <param name="container"></param>
        public Container(global::Autofac.IContainer container)
        {
            _container = container;
        }


#pragma warning disable 1591
        public T Get<T>()
        {
            return _container.Resolve<T>();
        }

        public T Get<T>(bool optional)
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch
            {
                if (!optional) throw;
            }

            return default(T);
        }

        public object Get(Type type)
        {
            //nasty hack, because autofac doesn't create instance unknown type (and I thin it is good)
            object o = _container.ResolveOptional(type);
            return o ?? ResolveUnregistered(type);
        }

        public object Get(Type type, bool optional)
        {
            if (optional)
            {
                return _container.ResolveOptional(type);
            }
            return _container.Resolve(type);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _container.Resolve<IEnumerable<T>>().ToList();
        }

        public bool HasBindingFor(Type type)
        {
            return _container.IsRegistered(type);
        }

        public bool HasBindingFor<T>()
        {
            return _container.IsRegistered<T>();
        }

        public IEnumerable<object> GetAll(Type type)
        {
            List<object> list = ((IEnumerable) _container
                                                   .Resolve(typeof (IEnumerable<>)
                                                                .MakeGenericType(type)))
                .OfType<object>()
                .ToList();

            return list;
        }

        public IEnumerable<Type> GetBoundServices()
        {
            IEnumerable<Type> types = _container.ComponentRegistry.Registrations
                                                .SelectMany(r => r.Services.OfType<IServiceWithType>(),
                                                            (r, s) => new {r, s})
                                                .Select(rs => rs.r.Activator.LimitType).ToList();
            return types;
        }

        public void Bind(Type type, Func<Type> resolveCallback)
        {
            RegisterWithCallback(type, resolveCallback, DefaultLifecycle);
        }

        public void Bind<T>(Func<Type> resolveCallback)
        {
            RegisterWithCallback(typeof (T), resolveCallback, DefaultLifecycle);
        }


        public void Bind(Type type, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            RegisterWithCallback(type, resolveCallback, lifecycle);
        }

        public void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            RegisterWithCallback(typeof (T), resolveCallback, lifecycle);
        }

        public void Bind<T>(Type type)
        {
            RegisterService(typeof (T), type, DefaultLifecycle);
        }

        public void Bind(Type service, Type type)
        {
            RegisterService(service, type, DefaultLifecycle);
        }

        public void Bind<T>(Type type, BindingLifecycle lifecycle)
        {
            RegisterService(typeof (T), type, lifecycle);
        }

        public void Bind(Type service, Type type, BindingLifecycle lifecycle)
        {
            RegisterService(service, type, lifecycle);
        }

        public void Bind<T>(T instance)
        {
            RegisterInstance(typeof (T), instance);
        }

        public void Bind(Type service, object instance)
        {
            RegisterInstance(service, instance);
        }


        public void Bind<T>(Func<T> resolveCallback)
        {
            RegisterWithCallback(typeof (T), resolveCallback, DefaultLifecycle);
        }

        public void Bind(Type service, Func<object> resolveCallback)
        {
            RegisterWithCallback(service, resolveCallback, DefaultLifecycle);
        }

        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            RegisterWithCallback(typeof (T), resolveCallback, DefaultLifecycle);
        }

        public void Bind(Type service, Func<object> resolveCallback, BindingLifecycle lifecycle)
        {
            RegisterWithCallback(service, resolveCallback, DefaultLifecycle);
        }

        public BindingLifecycle DefaultLifecycle { get; set; }

#pragma warning restore 1591

        void RegisterWithCallback<T>(Type service, Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            Update(x => x.Register(c => resolveCallback()).PerLifeStyle(lifecycle).As(service));
        }

        void RegisterWithCallback(Type type, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            Update(x => x.Register(c => resolveCallback()).PerLifeStyle(lifecycle).As(type));
        }

        void RegisterService(Type service, Type type, BindingLifecycle lifecycle)
        {
            Update(x =>
                {
                    if (type.IsGenericType)
                    {
                        x.RegisterGeneric(type).PerLifeStyle(lifecycle).As(service);
                    }
                    else
                    {
                        x.RegisterType(type).PerLifeStyle(lifecycle).As(service);
                    }
                });
        }

        void RegisterInstance(Type service, object instance)
        {
            Update(x => x.RegisterInstance(instance).As(service));
        }

        void Update(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            action.Invoke(builder);
            builder.Update(_container);
        }


        object ResolveUnregistered(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
            {
                try
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (ParameterInfo parameter in parameters)
                    {
                        object service = _container.Resolve(parameter.ParameterType);
                        if (service == null) throw new Exception("Unkown service");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (Exception)
                {
                }
            }
            throw new MissingDefaultConstructorException(type);
        }
    }
}