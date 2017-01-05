/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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