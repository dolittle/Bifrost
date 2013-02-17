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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bifrost.Events
{
	/// <summary>
	/// Extensions for <see cref="EventSource"/>
	/// </summary>
    public static class EventSourceExtensions
	{
#pragma warning disable 1591 // Xml Comments
		public static class EventSourceHandleMethods<T>
        {
            public static readonly Dictionary<Type, MethodInfo> MethodsPerEventType = new Dictionary<Type, MethodInfo>();

            static EventSourceHandleMethods()
            {
                var eventSourceType = typeof(T);

                Func<MethodInfo, bool> hasEventParameter = (m) =>
                                                               {
                                                                   var parameters = m.GetParameters();
                                                                   if (parameters.Length != 1)
                                                                       return false;
#if(NETFX_CORE)
                                                                   
                                                                   return typeof(IEvent).GetTypeInfo().IsAssignableFrom(parameters.Single().ParameterType.GetTypeInfo());
#else
                                                                   return typeof(IEvent).IsAssignableFrom(parameters.Single().ParameterType);
#endif
                                                               };

#if(NETFX_CORE)
                var methods = eventSourceType.GetTypeInfo().DeclaredMethods.Where(m => m.Name.Equals("Handle") && hasEventParameter(m));
#else
                var methods = eventSourceType.GetMethods(BindingFlags.NonPublic|BindingFlags.Instance).Where(m => m.Name.Equals("Handle") && hasEventParameter(m));
#endif
                foreach (var method in methods)
                    MethodsPerEventType[method.GetParameters()[0].ParameterType] = method;
            }
        }


        private static Dictionary<Type, MethodInfo> GetHandleMethodsFor(Type eventSourceType)
        {
            var methods = typeof(EventSourceHandleMethods<>)
                              .MakeGenericType(eventSourceType)
#if(NETFX_CORE)
                              .GetRuntimeField("MethodPerEventType")
#else
                              .GetField("MethodsPerEventType", BindingFlags.Public | BindingFlags.Static)
#endif
                              .GetValue(null) as Dictionary<Type, MethodInfo>;
            return methods;


        }

#pragma warning restore 1591 // Xml Comments


		/// <summary>
		/// Get handle method from an <see cref="EventSource"/> for a specific <see cref="IEvent"/>, if any
		/// </summary>
		/// <param name="eventSource"><see cref="EventSource"/> to get method from</param>
		/// <param name="event"><see cref="IEvent"/> to get method for</param>
		/// <returns><see cref="MethodInfo"/> containing information about the handle method, null if none exists</returns>
		public static MethodInfo GetHandleMethod(this EventSource eventSource, IEvent @event)
        {
            var eventType = @event.GetType();
            var handleMethods = GetHandleMethodsFor(eventSource.GetType());
            return handleMethods.ContainsKey(eventType) ? handleMethods[eventType] : null;
        }

        /// <summary>
        /// Indicates whether the Event Source maintains state and requires to handles events to restore that state
        /// </summary>
        /// <param name="eventSource"><see cref="EventSource"/> to test for state</param>
        /// <returns>true if the Event Source does not maintain state</returns>
        public static bool IsStateless(this EventSource eventSource)
        {
            return GetHandleMethodsFor(eventSource.GetType()).Count == 0;
        }
    }
}