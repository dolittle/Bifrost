/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.ComponentModel;
using Bifrost.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an <see cref="IInterceptor"/> that intercepts all calls for <see cref="ICommandFor{T}"/> proxies
    /// </summary>
    public class CommandForProxyInterceptor : Interceptor, ICommandForProxyInterceptor
    {
        /// <summary>
        /// Initalizes an instance of <see cref="CommandForProxyInterceptor"/>
        /// </summary>
        public CommandForProxyInterceptor(
            ICanHandleInvocationsFor<System.Windows.Input.ICommand, CommandInvocationHandler> commandInvocation,
            ICanHandleInvocationsFor<INotifyDataErrorInfo, CommandNotifyDataErrorInfoHandler> commandNotifyDataErrorInfo,
            ICanHandleInvocationsFor<ICommandProcess, CommandProcessHandler> commandProcess)
        {
            AddHandler(commandInvocation);
            AddHandler(commandNotifyDataErrorInfo);
            AddHandler(commandProcess);
        }


#pragma warning disable 1591 // Xml Comments
        public override void OnIntercept(IInvocation invocation)
        {
            if (HandleCommandInstanceIfInvocationMatches(invocation)) return;

            if (invocation.Method.DeclaringType == typeof(ICanProcessCommandProcess))
            {
                invocation.Proceed();
                return;
            }

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
        }
#pragma warning restore 1591 // Xml Comments


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
}
