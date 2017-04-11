using System.Dynamic;
using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_TenantManager
{
    public class when_getting_current_and_there_is_none_in_the_call_context : given.a_tenant_manager
    {
        static ITenant result;
        static TenantId tenant_id;

        Establish context = () =>
        {
            tenant_id = "42";
            tenant_id_resolver.Setup(t => t.Resolve()).Returns(tenant_id);
        };

        Because of = () => result = tenant_manager.Current;

        It should_return_a_tenant = () => result.ShouldNotBeNull();
        It should_hold_the_correct_tenant_id = () => result.TenantId.ShouldEqual(tenant_id);
        It should_populate_tenant = () => tenant_populator.Verify(t => t.Populate(result, Moq.It.IsAny<DynamicObject>()), Moq.Times.Once());
        It should_store_tenant_in_call_context = () => call_context.Verify(c => c.SetData(TenantManager.TenantKey, result), Moq.Times.Once());
    }
}
