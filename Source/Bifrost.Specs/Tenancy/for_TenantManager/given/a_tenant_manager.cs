using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_TenantManager.given
{
    public class a_tenant_manager : all_dependencies
    {
        protected static TenantManager tenant_manager;

        Establish context = () => 
            tenant_manager = 
                new TenantManager(
                    call_context.Object,
                    tenant_populator.Object,
                    tenant_id_resolver.Object
                );
    }
}
