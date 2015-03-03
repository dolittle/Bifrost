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
using System.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Reflection
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanHandleInvocationsFor{T1,T2}"/> that uses reflection
    /// to discover wether or not it can handle an invocation and then also handles it through reflection
    /// </summary>
    /// <typeparam name="TInterface">Interface to handle</typeparam>
    /// <typeparam name="TImplementation">Implementation that actually will do the handling</typeparam>
    /// <remarks>
    /// There are two interfaces that is interesting here that your implementation can implement:
    /// <see cref="INeedTargetInstance"/> and <see cref="INeedProxyInstance"/>
    /// </remarks>
    public class InvocationHandler<TInterface, TImplementation> : ICanHandleInvocationsFor<TInterface, TImplementation>
        where TImplementation : TInterface
    {
        const string TargetInstancePropertyName = "TargetInstance";

        TImplementation _implementation;

        /// <summary>
        /// Initializes a new instance of <see cref="InvocationHandler"/>
        /// </summary>
        /// <param name="implementation"></param>
        public InvocationHandler(TImplementation implementation)
        {
            _implementation = implementation;
        }

#pragma warning disable 1591 // Xml Comments
        public bool CanHandle(IInvocation invocation)
        {
            return invocation.Method.DeclaringType == typeof(TInterface);
        }

        public void Handle(IInvocation invocation)
        {
            if (_implementation is INeedTargetInstance)
            {
                var property = typeof(TImplementation).GetProperty(TargetInstancePropertyName, BindingFlags.Instance | BindingFlags.Public);
                if (property.PropertyType != typeof(TInterface)) throw new TargetInstanceTypeMismatch(property.PropertyType, typeof(TInterface));

                property.SetValue(_implementation, invocation.Proxy);
            }
            if (_implementation is INeedProxyInstance) ((INeedProxyInstance)_implementation).Proxy = invocation.Proxy;
            invocation.ReturnValue = invocation.Method.Invoke(_implementation, invocation.Arguments);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
