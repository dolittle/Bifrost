using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
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
                    var raiseMethod = @event.GetRaiseMethod();
                    raiseMethod.Invoke(proxy, null);
                };
        }


        public void Intercept(IInvocation invocation)
        {
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
