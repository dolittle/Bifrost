using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_BindingConventionManager.given
{
    public class a_binding_convention_manager_with_one_type : a_binding_convention_manager
    {
        protected static Type service_type;

        Establish context = () =>
        {
            service_type = typeof (IService);
            GetMock<ITypeDiscoverer>().Setup(t => t.GetAll()).Returns(new[] {service_type});
        };
    }
}