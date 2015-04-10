using System;
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Configuration
{
    public class DesktopConfiguration : IFrontendTargetConfiguration
    {
        public void Initialize(IContainer container)
        {
            container.Bind(typeof(ICommandFor<>), (Type t) =>
            {
                var commandForProxies = container.Get<ICommandForProxies>();
                return commandForProxies.CallGenericMethod<object, ICommandForProxies>(cc => cc.GetFor<Bifrost.Commands.Command>, t.GenericTypeArguments[0]);
            });

            var dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            Bifrost.Execution.DispatcherManager.Current = new Bifrost.Execution.Dispatcher(dispatcher);
            container.Bind<System.Windows.Threading.Dispatcher>(dispatcher);
            Configure.Instance.Container.Bind<IDispatcher>(Bifrost.Execution.DispatcherManager.Current);

            var proxyGenerator = new ProxyGenerator();
            Configure.Instance.Container.Bind<IProxyBuilder>(proxyGenerator.ProxyBuilder);
            Configure.Instance.Container.Bind(typeof(ICanHandleInvocationsFor<,>), typeof(InvocationHandler<,>));
        }
    }
}
