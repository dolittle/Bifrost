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
using System.Reflection;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a delegate that is weakly referenced - non obtrusive for the
    /// garbage collector
    /// </summary>
    public class WeakDelegate
    {
        WeakReference _target;
        MethodInfo _method;

        /// <summary>
        /// Initializes a new instance of <see cref="WeakDelegate"/>
        /// </summary>
        /// <param name="delegate"></param>
        public WeakDelegate(Delegate @delegate)
        {
            _target = new WeakReference(@delegate.Target);
            _method = @delegate.GetMethodInfo();
        }

        /// <summary>
        /// Gets whether or not the reference is alive
        /// </summary>
        public bool IsAlive { get { return _target.IsAlive || IsStatic; } }

        /// <summary>
        /// Gets the target instance
        /// </summary>
        public object Target { get { return _target.Target; } }

        /// <summary>
        /// Dynamically invoke the delegate on the target if the target is alive
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public object DynamicInvoke(params object[] arguments)
        {
            ThrowIfTargetNotAlive();
            ThrowIfSignatureMismatches(arguments);

            return _method.Invoke(Target, arguments);
        }

        bool IsStatic { get { return (_method.Attributes & MethodAttributes.Static) == MethodAttributes.Static; } }

        void ThrowIfTargetNotAlive()
        {
            if (!IsAlive) throw new CannotInvokeMethodBecauseTargetIsNotAlive(_method);
        }

        void ThrowIfSignatureMismatches(object[] arguments)
        {
            var parameters = _method.GetParameters();
            if (arguments.Length != parameters.Length) throw new InvalidSignatureException(_method);

            for (var argumentIndex = 0; argumentIndex < arguments.Length; argumentIndex++)
            {
                if (!parameters[argumentIndex].ParameterType.GetTypeInfo().IsAssignableFrom(arguments[argumentIndex].GetType())) throw new InvalidSignatureException(_method);
            }
        }
    }
}