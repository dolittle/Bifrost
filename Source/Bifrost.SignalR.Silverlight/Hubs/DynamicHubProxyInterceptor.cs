using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Newtonsoft.Json.Linq;
using SignalR.Client.Hubs;

namespace Bifrost.SignalR.Silverlight.Hubs
{
    public class DynamicHubProxyInterceptor : IInterceptor
    {
        IHubProxy _proxy;
        Type _type;
        MethodInfo _genericInvokeMethod;
        MethodInfo _invokeMethod;
        HubConnection _connection;
        Dictionary<string, List<Delegate>> _events = new Dictionary<string, List<Delegate>>();
        MethodInfo _toObjectMethod = typeof(JToken).GetMethod("ToObject", new Type[0]);

        public DynamicHubProxyInterceptor(HubConnection connection, Type type)
        {
            _type = type;
            var name = type.Name.Substring(1);
            _proxy = connection.CreateProxy(name);
            _connection = connection;
            _genericInvokeMethod = _proxy.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.Name == "Invoke" && m.ContainsGenericParameters).SingleOrDefault();
        }

        public void SetupEventSubscriptions(DynamicHubProxyInterceptor interceptor, IDynamicHubProxy proxy, Type proxyType)
        {
            var events = proxyType.GetEvents(BindingFlags.Public | BindingFlags.Instance);
            foreach (var @event in events)
                _proxy.Subscribe(@event.Name).Data += (tokens) =>
                {
                    if( _events.ContainsKey(@event.Name)) { 
                        var eventMethods = _events[@event.Name];
                        foreach (var eventMethod in eventMethods)
                        {
                            var actualParameters = new List<object>();

                            var parameters = eventMethod.Method.GetParameters();
                            for( var parameterIndex=0; parameterIndex<parameters.Length; parameterIndex++ ) 
                            {
                                var actualMethod = _toObjectMethod.MakeGenericMethod(parameters[parameterIndex].ParameterType);
                                var actualParameter = actualMethod.Invoke(tokens[parameterIndex], null);
                                actualParameters.Add(actualParameter);
                            }

                            eventMethod.DynamicInvoke(actualParameters.ToArray());
                        }
                    }
                };
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name.StartsWith("add_"))
            {
                var eventName = invocation.Method.Name.Substring(4);

                List<Delegate> events;
                if (!_events.ContainsKey(eventName))
                {
                    events = new List<Delegate>();
                    _events[eventName] = events;
                }
                else
                    events = _events[eventName];

                events.Add((Delegate)invocation.Arguments[0]);

                return;
            }

            if( _invokeMethod == null ) {
                var returnType = invocation.Method.ReturnType.GetGenericArguments()[0];
                _invokeMethod = _genericInvokeMethod.MakeGenericMethod(returnType);
            }

            var arguments = new List<object>();
            arguments.Add(invocation.Method.Name);
            arguments.Add(invocation.Arguments);

            invocation.ReturnValue = _invokeMethod.Invoke(_proxy, arguments.ToArray());
        }
    }
}
