using System;
using Bifrost.Tenancy;

namespace SimpleWeb
{
    public class TenantPopulator : ICanPopulateTenant
    {
        public void Populate(ITenant tenant, dynamic details)
        {
            var i = 0;
            i++;
        }
    }
}
