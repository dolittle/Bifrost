using System.Security.Principal;
using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ExecutionContextFactory
{
    public class when_creating
    {
        static ExecutionContextFactory factory;
        static Mock<ICanResolvePrincipal> identity_resolver_mock;
        static IExecutionContext instance;
        static IPrincipal principal;

        Establish context = () => 
        {
            principal = new GenericPrincipal(new GenericIdentity("Hello"),new string[0]);
            identity_resolver_mock = new Mock<ICanResolvePrincipal>();
            identity_resolver_mock.Setup(i => i.Resolve()).Returns(principal);
            factory = new ExecutionContextFactory(identity_resolver_mock.Object);
        };

        Because of = () => instance = factory.Create();

        It should_create_an_instance = () => instance.ShouldNotBeNull();
        It should_create_with_the_resolved_identity = () => instance.Principal.ShouldEqual(principal);

    }
}
