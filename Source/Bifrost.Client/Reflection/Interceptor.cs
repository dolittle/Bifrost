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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Configuration;
using Bifrost.Execution;
using Castle.DynamicProxy;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Represents an implementation of <see cref="IInterceptor"/> that enables 
    /// us to work more easily with invocation interception
    /// </summary>
    public class Interceptor : IInterceptor
    {
        List<ICanHandleInvocations> _invocationHandlers = new List<ICanHandleInvocations>();

        /// <summary>
        /// Add a handler for the given interface and a matching implementation 
        /// that will act as the handler
        /// </summary>
        /// <typeparam name="TInterface">Interface to add for</typeparam>
        /// <typeparam name="TImplementation">Implementation that will actually handle invocations</typeparam>
        protected void AddHandler(ICanHandleInvocations invocationHandler)
        {
            _invocationHandlers.Add(invocationHandler);
        }


        /// <summary>
        /// Method to override if you need to intercept invocations that aren't automatically handled
        /// </summary>
        /// <param name="invocation"></param>
        public virtual void OnIntercept(IInvocation invocation) {}

        public void Intercept(IInvocation invocation)
        {
            var handler = _invocationHandlers.FirstOrDefault(h => h.CanHandle(invocation));
            if (handler != null) handler.Handle(invocation);
            else OnIntercept(invocation);
        }
    }
}
