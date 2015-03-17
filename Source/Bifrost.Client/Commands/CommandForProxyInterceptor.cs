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
            ICanHandleInvocationsFor<ICommandProcess, CommandProcessHandler> commandProcess,
            ICanHandleInvocationsFor<ICanProcessCommandProcess, CommandProcessProcessor> commandProcessProcessor)
        {
            AddHandler(commandInvocation);
            AddHandler(commandNotifyDataErrorInfo);
            AddHandler(commandProcess);
            AddHandler(commandProcessProcessor);
        }


#pragma warning disable 1591 // Xml Comments
        public override void OnIntercept(IInvocation invocation)
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
