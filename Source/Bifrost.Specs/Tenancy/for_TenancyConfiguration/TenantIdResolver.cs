using Bifrost.Tenancy;

namespace Bifrost.Specs.Tenancy.for_TenancyConfiguration
{
    public class TenantIdResolver : ICanResolveTenantId
    {
        public TenantId Resolve()
        {
            return "42";
        }
    }
}
