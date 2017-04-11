using System;
using Bifrost.Tenancy;
using Machine.Specifications;

namespace Bifrost.Specs.Tenancy.for_TenancyConfiguration
{
    public class when_configuring_with_multiple_custom_tenant_id_resolvers : given.no_tenant_id_resolvers
    {
        static Exception exception;

        Establish context = () =>
        {
            resolvers.Add(typeof(TenantIdResolver));
            resolvers.Add(typeof(SecondTenantIdResolver));
        };

        Because of = () => exception = Catch.Exception(() => configuration.Initialize(container.Object));

        It should_throw_multiple_tenant_id_resolvers_found = () => exception.ShouldBeOfExactType<MultipleTenantIdResolversFound>();
    }
}
