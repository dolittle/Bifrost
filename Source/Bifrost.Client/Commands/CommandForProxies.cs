/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.ComponentModel;
using Bifrost.Execution;
using Bifrost.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandForProxies"/>
    /// </summary>
    [Singleton]
    public class CommandForProxies : ICommandForProxies
    {
        static class ProxyTypePerCommand<T>
        {
            public static Type ProxyType { get; set; }
        }

        IProxyBuilder _proxyBuilder;
        IProxying _proxying;
        ICommandForProxyInterceptor _interceptor;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandForProxies"/>
        /// </summary>
        public CommandForProxies(IProxying proxying, IProxyBuilder proxyBuilder, ICommandForProxyInterceptor interceptor)
        {
            _proxying = proxying;
            _proxyBuilder = proxyBuilder;
            _interceptor = interceptor;
        }


#pragma warning disable 1591 // Xml Comments
        public ICommandFor<T> GetFor<T>() where T : ICommand, new()
        {
            var command = new T();
            return GetForInstance(command);
        }

        public ICommandFor<T> GetFor<T>(T instance) where T : ICommand
        {
            return GetForInstance(instance);
        }

        ICommandFor<T> GetForInstance<T>(T instance) where T : ICommand
        {
            var type = GetProxyTypeFor<T>();

            var proxied = Activator.CreateInstance(type, new[] { 
                new IInterceptor[] { 
                    _interceptor,
                } 
            }) as ICommandFor<T>;

            ((IHoldCommandInstance)proxied).CommandInstance = instance;

            return proxied;
        }

        Type GetProxyTypeFor<T>() where T : ICommand
        {
            if (ProxyTypePerCommand<T>.ProxyType == null)
            {
                var interfaceForCommandType = _proxying.BuildInterfaceWithPropertiesFrom(typeof(T));

                var options = new ProxyGenerationOptions();

                ProxyTypePerCommand<T>.ProxyType = _proxyBuilder.CreateClassProxyType(
                    typeof(CommandProxyInstance),
                    new[] { 
                    typeof(ICommandFor<T>), 
                    interfaceForCommandType, 
                    typeof(ICommandProcess),
                    typeof(ICanProcessCommandProcess),
                    typeof(System.Windows.Input.ICommand), 
                    typeof(INotifyDataErrorInfo),
                    typeof(IHoldCommandInstance) 
                }, options);
            }
            return ProxyTypePerCommand<T>.ProxyType;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
