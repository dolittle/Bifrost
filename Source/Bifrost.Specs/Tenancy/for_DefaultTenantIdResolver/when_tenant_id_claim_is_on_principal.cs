using System.Security.Claims;
using Bifrost.Tenancy;
using Machine.Specifications;


namespace Bifrost.Specs.Tenancy.for_DefaultTenantIdResolver
{
    public class when_tenant_id_claim_is_on_principal : given.a_default_tenant_id_resolver
    {
        const string tenant_id = "Some Tenant";
        static TenantId result;

        Establish context = () => ClaimsPrincipal.ClaimsPrincipalSelector = () =>
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(DefaultTenantIdResolver.TenantIdClaimType, tenant_id));
            var principal = new ClaimsPrincipal(identity);
            return principal;
        };

        Because of = () => result = resolver.Resolve();

        It should_return_tenant_id_in_claim = () => result.Value.ShouldEqual(tenant_id);
    }
}
