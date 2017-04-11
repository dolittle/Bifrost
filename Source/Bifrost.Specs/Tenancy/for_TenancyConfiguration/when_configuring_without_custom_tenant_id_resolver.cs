using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_TenancyConfiguration
{
    public class when_configuring_without_custom_tenant_id_resolver : given.no_tenant_id_resolvers
    {
        Because of = () => configuration.Initialize(container.Object);

        It should_bind_to_the_default_resolver = () => container.Verify(c => c.Bind<ICanResolveTenantId>(typeof(DefaultTenantIdResolver)), Moq.Times.Once());
    }
}
