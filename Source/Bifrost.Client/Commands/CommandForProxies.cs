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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Bifrost.Configuration;
using Bifrost.Execution;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandForProxies"/>
    /// </summary>
    [Singleton]
    public class CommandForProxies : ICommandForProxies
    {
        private const string DynamicAssemblyName = "Dynamic Assembly";
        private const string DynamicModuleName = "Dynamic Module";

        private static readonly AssemblyBuilder DynamicAssembly;
        private static readonly ModuleBuilder DynamicModule;

        static CommandForProxies()
        {
            var dynamicAssemblyName = CreateUniqueName(DynamicAssemblyName);
            var dynamicModuleName = CreateUniqueName(DynamicModuleName);
            var appDomain = Thread.GetDomain();
            var assemblyName = new AssemblyName(dynamicAssemblyName);
            DynamicAssembly = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            DynamicModule = DynamicAssembly.DefineDynamicModule(dynamicModuleName, true);
        }

        static string CreateUniqueName(string prefix)
        {
            var uid = Guid.NewGuid().ToString();
            uid = uid.Replace('-', '_');
            var name = string.Format("{0}{1}", prefix, uid);
            return name;
        }


        static TypeBuilder DefineType(Type type)
        {
            var name = CreateUniqueName(type.Name);
            var typeBuilder = DynamicModule.DefineType(name, TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.Serializable);
            return typeBuilder;
        }


        ProxyGenerator _proxyGenerator;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandForProxies"/>
        /// </summary>
        public CommandForProxies()
        {
            _proxyGenerator = new ProxyGenerator();
        }


#pragma warning disable 1591 // Xml Comments

        public class CommandInstanceHolder : IHoldCommandInstance
        {
            public ICommand CommandInstance { get; set; }
        }

        public interface IHoldCommandInstance
        {
            ICommand CommandInstance { get; set; }
        }


        public class CommandForProxyInterceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                if (HandleCommandInstanceIfInvocationMatches(invocation)) return;

                var commandInstanceHolder = invocation.Proxy as IHoldCommandInstance;
                var instance = commandInstanceHolder.CommandInstance;

                if (invocation.Method.Name.StartsWith("get_") || invocation.Method.Name.StartsWith("set_"))
                {

                    if (invocation.Method.Name.Contains("get_Instance"))
                    {
                        invocation.ReturnValue = instance;
                        return;
                    }

                    if (invocation.Method.Name.StartsWith("get_"))
                    {
                        var getMethod = commandInstanceHolder.CommandInstance.GetType().GetMethod(invocation.Method.Name);
                        invocation.ReturnValue = getMethod.Invoke(instance, null);
                    }
                    if (invocation.Method.Name.StartsWith("set_"))
                    {
                        var setMethod = instance.GetType().GetMethod(invocation.Method.Name);
                        setMethod.Invoke(instance, invocation.Arguments);
                    }
                }

                var methods = typeof(System.Windows.Input.ICommand).GetMethods();
                var hasMethod = methods.Any(m => m.Name == invocation.Method.Name);
                if (!hasMethod) return;

                if (invocation.Method.Name.Equals("CanExecute"))
                {
                    invocation.ReturnValue = true;
                }
                else if (invocation.Method.Name.Equals("Execute"))
                {
                    var commandCoordinator = Configure.Instance.Container.Get<ICommandCoordinator>();
                    var result = commandCoordinator.Handle(instance);
                }
            }


            bool HandleCommandInstanceIfInvocationMatches(IInvocation invocation)
            {
                if (invocation.Method.Name.Equals("set_CommandInstance"))
                {
                    invocation.Proceed();
                    return true;
                }
                if (invocation.Method.Name.Equals("get_CommandInstance"))
                {
                    invocation.Proceed();
                    return true;
                }
                return false;
            }

        }

        public ICommandFor<T> GetFor<T>() where T : ICommand, new()
        {
            var command = new T();
            
            var interfaceForCommandType = GetInterfaceForCommandType<T>();

            var options = new ProxyGenerationOptions();
            var commandForInterceptor = new CommandForProxyInterceptor();

            var type = _proxyGenerator.ProxyBuilder.CreateClassProxyType(
                typeof(CommandInstanceHolder), 
                new[] { 
                    typeof(ICommandFor<T>), 
                    interfaceForCommandType, 
                    typeof(System.Windows.Input.ICommand), 
                    typeof(IHoldCommandInstance) 
                }, options);

            var i = Activator.CreateInstance(type, new[] { 
                new IInterceptor[] { 
                    commandForInterceptor,
                } 
            }) as ICommandFor<T>;

            ((IHoldCommandInstance)i).CommandInstance = command;

            return i;
        }

        static Type GetInterfaceForCommandType<T>() where T : ICommand, new()
        {
            var typeBuilder = DefineType(typeof(T));

            foreach (var property in typeof(T).GetProperties())
            {
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.None, property.PropertyType, new Type[0]);
                var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual, property.PropertyType, new Type[0]);
                propertyBuilder.SetGetMethod(getMethodBuilder);
                var setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual, property.PropertyType, new[] { property.PropertyType });
                propertyBuilder.SetSetMethod(setMethodBuilder);
            }

            var interfaceForCommandType = typeBuilder.CreateType();
            return interfaceForCommandType;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
