using System;
using Bifrost.Execution;
using Bifrost.Notification;

namespace Bifrost.Mimir
{
    public class ViewModelConvention : BaseConvention
    {
        private readonly NotifyingObjectWeaver _weaver;

        public ViewModelConvention()
        {
            _weaver = new NotifyingObjectWeaver();
        }

        public override bool CanResolve(Type service)
        {
            return service.Name.Equals("ViewModel");
        }

        public override void Resolve(IContainer container, Type service)
        {
            var proxy = _weaver.GetProxyType(service);
            container.Bind(service,proxy);
        }
    }
}