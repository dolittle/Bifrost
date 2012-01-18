using System;
using Bifrost.Execution;

namespace Bifrost.Specs.Execution.for_BindingConventionManager
{
    public class FakeBindingConvention : IBindingConvention
    {
        public bool CanResolveCalled = false;
        public bool CanResolveProperty = false;
        public bool CanResolve(Type service)
        {
            CanResolveCalled = true;
            return CanResolveProperty;
        }


        public bool ResolveCalled = false;
        public void Resolve(IContainer container, Type service)
        {
            ResolveCalled = true;
        }
    }
}