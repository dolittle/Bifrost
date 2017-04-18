using Bifrost.Tenancy;

namespace Bifrost.Specs.Tenancy.for_TenancyConfiguration
{
    public class SecondTenantIdResolver : ICanResolveTenantId
    {
        public TenantId Resolve()
        {
            return "42";
        }
    }
}
