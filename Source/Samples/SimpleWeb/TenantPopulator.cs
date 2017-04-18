using System;
using Bifrost.Tenancy;

namespace SimpleWeb
{
    public class TenantPopulator : ICanPopulateTenant
    {
        public void Populate(ITenant tenant, dynamic details)
        {
            details.SomethingTenantSpecific = $"This is a tenant specific string : {tenant.TenantId}";
        }
    }
}
