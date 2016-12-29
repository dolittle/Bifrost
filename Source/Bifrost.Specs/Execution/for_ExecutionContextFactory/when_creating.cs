using System.Dynamic;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.Tenancy;
using Bifrost.Testing;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ExecutionContextFactory
{
    public class when_creating : dependency_injection
    {
        static ExecutionContextFactory factory;
        static IExecutionContext instance;
        static IPrincipal principal;

        Establish context = () => 
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("nb-NO");

            principal = new GenericPrincipal(new GenericIdentity("Hello"),new string[0]);
            GetMock<ICanResolvePrincipal>().Setup(i => i.Resolve()).Returns(principal);
            GetMock<IConfigure>().SetupGet(s => s.SystemName).Returns("Something");
            GetMock<ITenantManager>().SetupGet(s=>s.Current).Returns(Get<ITenant>());

            factory = Get<ExecutionContextFactory>();
        };

        Because of = () => instance = factory.Create();

        It should_create_an_instance = () => instance.ShouldNotBeNull();
        It should_create_with_the_resolved_identity = () => instance.Principal.ShouldEqual(principal);
        It should_populate_details = () => GetMock<IExecutionContextDetailsPopulator>().Verify(d => d.Populate(instance, Moq.It.IsAny<DynamicObject>()), Moq.Times.Once());
        It should_be_initialized_with_the_current_threads_culture = () => instance.Culture.ShouldEqual(Thread.CurrentThread.CurrentCulture);
        It should_be_initialized_with_the_configured_system_name = () => instance.System.ShouldEqual("Something");
        It should_be_initialized_with_the_current_tenant = () => instance.Tenant.ShouldEqual(Get<ITenant>());
    }
}
