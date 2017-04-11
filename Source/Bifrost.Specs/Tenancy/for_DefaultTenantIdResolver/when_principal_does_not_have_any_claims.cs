using System.Security.Claims;
using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_DefaultTenantIdResolver
{
    public class when_principal_does_not_have_any_claims : given.a_default_tenant_id_resolver
    {
        static TenantId result;

        Establish context = () => ClaimsPrincipal.ClaimsPrincipalSelector = () =>
        {
            var identity = new ClaimsIdentity();
            var principal = new ClaimsPrincipal(identity);
            return principal;
        };

        Because of = () => result = resolver.Resolve();

        It should_return_unknown_tenant = () => result.Value.ShouldEqual(DefaultTenantIdResolver.UnknownTenantId);
    }
}
