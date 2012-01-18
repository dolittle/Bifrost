using System;
using Bifrost.Execution;

namespace Bifrost.Specs.Execution.for_any_conventions
{
    public class SelfBindingConvention : BaseConvention
    {
        public override bool CanResolve(Type service)
        {
            return true;
        }

        public override void Resolve(IContainer container, Type service)
        {
            container.Bind(service, service, GetScopeForTarget(service));
        }
    }
}
