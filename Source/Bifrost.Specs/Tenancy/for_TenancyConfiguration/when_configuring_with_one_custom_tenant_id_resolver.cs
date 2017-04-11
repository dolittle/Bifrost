using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_TenancyConfiguration
{
    public class when_configuring_with_one_custom_tenant_id_resolver : given.no_tenant_id_resolvers
    {
        Establish context = () => resolvers.Add(typeof(TenantIdResolver));

        Because of = () => configuration.Initialize(container.Object);

        It should_bind_to_dicscovered_resolver = () => container.Verify(c => c.Bind<ICanResolveTenantId>(typeof(TenantIdResolver)), Moq.Times.Once());
    }
}
