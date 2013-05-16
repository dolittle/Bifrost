using System.Globalization;
using System.Security.Principal;
using System.Threading;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.Tenancy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ExecutionContextFactory
{
    public class when_creating
    {
        static ExecutionContextFactory factory;
        static Mock<ICanResolvePrincipal> identity_resolver_mock;
        static Mock<IExecutionContextDetailsPopulator> details_populator_mock;
        static Mock<IConfigure> configure_mock;
        static Mock<ITenantManager> tenant_manager_mock;
        static Mock<ITenant> tenant_mock;
        static IExecutionContext instance;
        static IPrincipal principal;

        Establish context = () => 
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("nb-NO");

            principal = new GenericPrincipal(new GenericIdentity("Hello"),new string[0]);
            identity_resolver_mock = new Mock<ICanResolvePrincipal>();
            identity_resolver_mock.Setup(i => i.Resolve()).Returns(principal);

            details_populator_mock = new Mock<IExecutionContextDetailsPopulator>();

            configure_mock = new Mock<IConfigure>();
            configure_mock.SetupGet(s => s.SystemName).Returns("Something");

            tenant_mock = new Mock<ITenant>();
            tenant_manager_mock = new Mock<ITenantManager>();
            tenant_manager_mock.SetupGet(s=>s.Current).Returns(tenant_mock.Object);

            factory = new ExecutionContextFactory(identity_resolver_mock.Object, details_populator_mock.Object, configure_mock.Object, tenant_manager_mock.Object);
        };

        Because of = () => instance = factory.Create();

        It should_create_an_instance = () => instance.ShouldNotBeNull();
        It should_create_with_the_resolved_identity = () => instance.Principal.ShouldEqual(principal);
        It should_populate_details = () => details_populator_mock.Verify(d => d.Populate(instance, instance.Details), Times.Once());
        It should_be_initialized_with_the_current_threads_culture = () => instance.Culture.ShouldEqual(Thread.CurrentThread.CurrentCulture);
        It should_be_initialized_with_the_configured_system_name = () => instance.System.ShouldEqual("Something");
        It should_be_initialized_with_the_current_tenant = () => instance.Tenant.ShouldEqual(tenant_mock.Object);
    }
}
