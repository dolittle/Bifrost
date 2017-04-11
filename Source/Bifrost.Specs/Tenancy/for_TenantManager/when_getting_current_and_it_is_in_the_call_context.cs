using Bifrost.Tenancy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tenancy.for_TenantManager
{
    public class when_getting_current_and_it_is_in_the_call_context : given.a_tenant_manager
    {
        static ITenant tenant;
        static ITenant result;

        Establish context = () =>
        {
            tenant = Mock.Of<ITenant>();
            call_context.Setup(c => c.HasData(TenantManager.TenantKey)).Returns(true);
            call_context.Setup(c => c.GetData<ITenant>(TenantManager.TenantKey)).Returns(tenant);
        };

        Because of = () => result = tenant_manager.Current;

        It should_return_the_tenant_in_the_call_context = () => result.ShouldEqual(tenant);
    }
}
