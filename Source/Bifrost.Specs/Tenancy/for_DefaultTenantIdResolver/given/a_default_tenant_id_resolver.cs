using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_DefaultTenantIdResolver.given
{
    public class a_default_tenant_id_resolver
    {
        protected static DefaultTenantIdResolver resolver;

        Establish context = () => resolver = new DefaultTenantIdResolver();
    }
}
