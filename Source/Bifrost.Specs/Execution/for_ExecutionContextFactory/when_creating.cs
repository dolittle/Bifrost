using System.Dynamic;
using System.Globalization;
using System.Security.Claims;
using Bifrost.Applications;
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
        static Mock<ICanResolvePrincipal> identity_resolver;
        static Mock<IExecutionContextDetailsPopulator> details_populator;
        static Mock<IApplication> application;
        static Mock<IContainer> container;
        static Mock<ITenant> tenant;
        static IExecutionContext instance;
        static ClaimsPrincipal principal;

        Establish context = () => 
        {
            CultureInfo.CurrentCulture = new CultureInfo("nb-NO");

            principal = new ClaimsPrincipal(new ClaimsIdentity());
            identity_resolver = new Mock<ICanResolvePrincipal>();
            identity_resolver.Setup(i => i.Resolve()).Returns(principal);

            details_populator = new Mock<IExecutionContextDetailsPopulator>();

            application = new Mock<IApplication>();
            container = new Mock<IContainer>();
            tenant = new Mock<ITenant>();

            container.Setup(c => c.Get<ITenant>()).Returns(tenant.Object);

            factory = new ExecutionContextFactory(identity_resolver.Object, details_populator.Object, application.Object, container.Object);
        };

        Because of = () => instance = factory.Create();

        It should_create_an_instance = () => instance.ShouldNotBeNull();
        It should_create_with_the_resolved_identity = () => instance.Principal.ShouldEqual(principal);
        It should_populate_details = () => details_populator.Verify(d => d.Populate(instance, Moq.It.IsAny<DynamicObject>()), Times.Once());
        It should_be_initialized_with_the_current_threads_culture = () => instance.Culture.ShouldEqual(CultureInfo.CurrentCulture);
        It should_be_initialized_with_the_configured_system_name = () => instance.Application.ShouldEqual(application.Object);
        It should_be_initialized_with_the_current_tenant = () => instance.Tenant.ShouldEqual(tenant.Object);
    }
}
