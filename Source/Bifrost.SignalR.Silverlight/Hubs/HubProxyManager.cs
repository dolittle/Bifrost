using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Bifrost.Execution;
using SignalR.Client.Hubs;
using Castle.DynamicProxy;
using Bifrost.Extensions;
using System.Reflection;

namespace Bifrost.SignalR.Silverlight.Hubs
{
    [Singleton]
    public class HubProxyManager : IHubProxyManager
    {
        ITypeDiscoverer _typeDiscoverer;
        HubConnection _connection;
        Dictionary<Type, IDynamicHubProxy> _proxies = new Dictionary<Type, IDynamicHubProxy>();

        public HubProxyManager(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
            var source = Application.Current.Host.Source;
            var url = string.Format("{0}://{1}{2}",
                source.Scheme,
                source.Host,
                source.Port == 80 ? string.Empty : ":" + source.Port);

            _connection = new HubConnection(url);
            CreateAllHubProxies();
            _connection.Start().Wait();
        }


        void CreateAllHubProxies()
        {
            var proxyGenerator = new ProxyGenerator();
            var proxyTypes = _typeDiscoverer.GetAll().Where(t=>t.HasInterface<IDynamicHubProxy>());
            foreach (var proxyType in proxyTypes)
            {
                var interceptor = new DynamicHubProxyInterceptor(_connection, proxyType);
                var proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget(proxyType, interceptor) as IDynamicHubProxy;
                interceptor.SetupEventSubscriptions(interceptor, proxy, proxyType);
                _proxies[proxyType] = proxy;
            }
        }


        public T Get<T>() where T : IDynamicHubProxy
        {
            var type = typeof(T);
            if (_proxies.ContainsKey(type))
                return (T)_proxies[type];

            return default(T);
        }
    }
}
